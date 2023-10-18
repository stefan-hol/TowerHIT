using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemie : MonoBehaviour
{

    [SerializeField] private int lives;
    [SerializeField] private int speed;
    [SerializeField] private int damage;
    [SerializeField] private int Gold;
    [SerializeField] private float EnemieHeigth;
    [SerializeField] private EnemyType Type;

    private Path _path;
    private WayPoint _currentWaypoint;
    private WayPoint End;

    private float wave = 0f;
    float DistanceToWaypoint = Mathf.Infinity;

    public Vector2 GetPathDistance() { return new(DistanceToWaypoint, wave); }
    public EnemyType GetTyping() { return Type; }
    public float GetHeigth() { return EnemieHeigth; }
    public float GetHealth() { return lives; }

    void Start()
    {
        SetupPath();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        DistanceToWaypoint = Vector3.Distance(transform.position, _currentWaypoint.GetPosition(EnemieHeigth));

        if (DistanceToWaypoint <= 0.3f)
        {
            if (End == _currentWaypoint)
            {
                PathComplete();
            }
            else
            {
                _currentWaypoint = _path.GetNextWaypoint(_currentWaypoint);
                transform.LookAt(_currentWaypoint.GetPosition(EnemieHeigth));
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
}
