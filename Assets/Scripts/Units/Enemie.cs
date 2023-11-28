using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemie : MonoBehaviour
{
    #region variables/GetSeters
    [SerializeField] private int lives;
    [SerializeField] private int damage;
    [SerializeField] private int Gold;
    [SerializeField] private float baseSpeed;
    [SerializeField] private float EnemieHeigth;
    [SerializeField] private EnemyType Type;
    private Path _path;
    private WayPoint _currentWaypoint;
    private WayPoint End;

    private float speed;
    private float wave = 0f;
    private float timer;
    private float walkTime;


    public Vector3 GetPathDistance() { return new(Vector3.Distance(transform.position, _currentWaypoint.GetPosition(EnemieHeigth)), wave); }
    public EnemyType GetTyping() { return Type; }
    public float GetHeigth() { return EnemieHeigth; }
    public float GetHealth() { return lives; }

    void Start()
    {
        SetupPath();
    }
    #endregion
    public Vector3 ShotLocation(float bulletTime, GameObject lit)
    {
        lit.transform.SetPositionAndRotation(transform.position, transform.rotation);
        float DistancetoShoot = Vector3.Distance(transform.position, transform.position + speed * bulletTime * Vector3.forward);
        float DistanceToWaypoint = Vector3.Distance(transform.position, _currentWaypoint.GetPosition(EnemieHeigth));
        if (DistancetoShoot > DistanceToWaypoint) 
        {
            float exces = DistancetoShoot - DistanceToWaypoint;
            lit.transform.position += DistanceToWaypoint * Vector3.forward;
            if(_currentWaypoint != End) 
            {
                WayPoint point = _path.GetNextWaypoint(_currentWaypoint);
                lit.transform.LookAt(point.transform.position);
                lit.transform.position += exces * Vector3.forward;
                return lit.transform.position;
            }
            print("kut");
        }

        return lit.transform.position += DistancetoShoot * Vector3.forward;
        //return ret;
    }
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
        walkTime -= Time.deltaTime;
        if (walkTime <= 0)
        {
            if (End == _currentWaypoint)
            {
                PathComplete();
            }
            else
            {
                _currentWaypoint = _path.GetNextWaypoint(_currentWaypoint);
                transform.LookAt(_currentWaypoint.GetPosition(EnemieHeigth));
                walkTime = Vector3.Distance(transform.position, _currentWaypoint.GetPosition(EnemieHeigth)) / speed;
                wave++;
            }
        }
    }

    void SetupPath()
    {
        _path = FindObjectOfType<Path>();
        _currentWaypoint = _path.GetPathStart();
        transform.LookAt(_currentWaypoint.GetPosition(EnemieHeigth));
        End = _path.GetPathEnd();
        speed = baseSpeed;
        walkTime = Vector3.Distance(transform.position, _currentWaypoint.GetPosition(EnemieHeigth)) / speed;
    }

    public void TakeDamage(int amount)
    {
        lives -= amount;
        if (lives <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        FindObjectOfType<Player>().SetGold(Gold);
        Destroy(gameObject);
    }

    private void PathComplete()
    {
        Destroy(gameObject);
        FindObjectOfType<Player>().TakeDamage(damage);
    }
    public void SetSpeed(float slow)
    {
        speed = baseSpeed * slow;
        HandleCoolDown();
    }


    protected void HandleCoolDown()
    {
        StartCoroutine(SpeedReset(2));
    }
    protected IEnumerator SpeedReset(float cd)
    {
        yield return new WaitForSeconds(cd);
        speed = baseSpeed;
    }

}
