using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BaseTower : MonoBehaviour
{
    [SerializeField] protected float radius;
    [SerializeField] private LayerMask _layer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Enemie[] GetAllEnemiesInRange(EnemyType type)
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, radius, _layer);
        if (colls.Length <= 0)
        {
            return null;
        }

        List<Enemie> enemiesInRange = new List<Enemie>();
        for (int i = 0; i < colls.Length; i++)
        {
            if (type == EnemyType.All || type == colls[i].GetComponent<Enemie>().GetType())
            {
                enemiesInRange.Add(colls[i].GetComponent<Enemie>());
            }
        }
        return enemiesInRange.ToArray();

    }

    public Enemie GetFirstEnemyInRange(EnemyType type)
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, radius, _layer);
        for (int i = 0; i < colls.Length; i++)
        {
            if(type == EnemyType.All || type == colls[i].GetComponent<Enemie>().GetType())
            {
                return colls[i].GetComponent<Enemie>();
            }
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
