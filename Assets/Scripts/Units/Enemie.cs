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

    [SerializeField] private Player _player;
    [SerializeField] private Path _path;
    private WayPoint _currentWaypoint;
    private WayPoint End;

    private int wave = 0;

    public Vector2 GetPathDistance()
    {
        float DistanceToWaypoint = Vector3.Distance(transform.position, _currentWaypoint.GetPosition(EnemieHeigth));
        Vector2 distance = new(DistanceToWaypoint, wave);
        return distance;
    }
    public EnemyType GetTyping() { return Type; }

    public float GetHeigth() { return EnemieHeigth; }

    void Start()
    {
        SetupPath();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        float DistanceToWaypoint = Vector3.Distance(transform.position, _currentWaypoint.GetPosition(EnemieHeigth));

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
        _player = FindObjectOfType<Player>();
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
        _player.SetGold(Gold);
        Destroy(gameObject);
    }

    private void PathComplete()
    {
        Destroy(gameObject);
        _player.TakeDamage(damage);
    }
}
