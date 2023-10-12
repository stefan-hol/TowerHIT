using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BaseTower : MonoBehaviour
{
    [SerializeField] protected float radius;
    [SerializeField] protected int goldCost;
    [SerializeField] protected EnemyType towerType;
    private LayerMask _layer;

    private void Start()
    {
       _layer = LayerMask.GetMask("Enemy");
    }

    protected Enemie[] GetAllEnemiesInRange(EnemyType type)
    {
        List<Enemie> enemiesInRange = new List<Enemie>();

        Collider[] colls = Physics.OverlapSphere(transform.position, radius, _layer);

        if (colls.Length <= 0)
        {
            return enemiesInRange.ToArray();
        }

        foreach (Collider coll in colls)
        {
            Enemie enemie = coll.GetComponent<Enemie>();

            if (type == EnemyType.All || type == enemie.GetTyping())
            {
                enemiesInRange.Add(enemie);
            }
        }

        return enemiesInRange.ToArray();
    }

    protected Enemie GetFirstEnemyInRange(EnemyType type)
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, radius, _layer);

        foreach (Collider coll in colls)
        {
            Enemie enemie = coll.GetComponent<Enemie>();

            if (type == EnemyType.All || type == enemie.GetTyping())
            {
                return enemie;
            }
        }

        return null;
    }



    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}


/*
private bool Camo = false;

private Enemy targetFirstEnemy()
{
    Collider[] colls = Physics.OverlapSphere(transform.position, Range, _layer);
    Enemie[] enemies = new List<Enemie>();

    if (colls.Length <= 0)
    {
        return enemies.ToArray();
    }

    foreach (Collider coll in colls)
    {
        Enemy enemy = coll.GetComponent<Enemy>();

        if (enemy.GetCamo() == false || enemy.GetCamo() == camo)
        {
            if(enemy.GetEnumie() == _targetConditions || _targetConditions == Enumies.All){enemies.Add(enemy);}
        }
    }
    
    if((enemy.GetCamo() == false || enemy.GetCamo() == camo) && (enemy.GetEnumie() == _targetConditions || _targetConditions == Enumies.All))


    return enemies.ToArray();
}
*/