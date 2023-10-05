using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemie : MonoBehaviour
{

    [SerializeField] private int lives;
    [SerializeField] private int speed;
    [SerializeField] private int damage;
    [SerializeField] private int Gold;
    [SerializeField] private EnemyType Type;
    private Player _player;
    private Path _path;
    private WayPoint _currentWaypoint;
    private WayPoint End;


    // Start is called before the first frame update
    void Start()
    {
        SetupPath();

    }
    public EnemyType GetType() { return Type; }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        float DistanceToWaypoint = Vector3.Distance(transform.position, _currentWaypoint.GetPosition());

        if (DistanceToWaypoint <= 0.2f)
        {
            if (End == _currentWaypoint)
            {
                PathComplete();
            }
            else
            {
                _currentWaypoint = _path.GetNextWaypoint(_currentWaypoint);
                transform.LookAt(_currentWaypoint.GetPosition());
            }
        }
    }

    void SetupPath()
    {
        _path = FindObjectOfType<Path>();
        _player = FindObjectOfType<Player>();
        _currentWaypoint = _path.GetPathStart();
        transform.LookAt(_currentWaypoint.GetPosition());
        End = _path.GetPathEnd();
    }

    public void TakeDamage(int amount)
    {
        lives -= amount;

        if (lives <= 0)
        {
            _player.Kill(Gold);
            Destroy(gameObject);
        }

    }

    private void Death()
    {
        Destroy(gameObject);
    }

    private void PathComplete()
    {
        Destroy(gameObject);
        _player.TakeDamage(damage);
    }
}
