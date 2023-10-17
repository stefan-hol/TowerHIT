using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BaseTower : MonoBehaviour
{
    [SerializeField] protected float radius;
    [SerializeField] protected float cd;
    [SerializeField] protected int goldCost;
    [SerializeField] protected int damage;
    [SerializeField] protected EnemyType towerType;

    private LayerMask _layer;

    protected bool canFire = true;

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

        Enemie firstEnemie = null;
        Vector2 best = new(0, -1);

        foreach (Collider coll in colls)
        {
            Enemie enemie = coll.GetComponent<Enemie>();
            if (type == EnemyType.All || type == enemie.GetTyping())
            {
                Vector2 enemieDistance = enemie.GetPathDistance();
                if (enemieDistance == null) { break; }
                if (enemieDistance.y > best.y || (enemieDistance.y == best.y && enemieDistance.x < best.x))
                {
                    firstEnemie = enemie;
                    best = enemieDistance;
                }
            }
        }
        return firstEnemie;
    }

    protected void HandleCoolDown()
    {
        StartCoroutine(CooldownRoutine(cd));
    }
    protected IEnumerator CooldownRoutine(float cd)
    {
        yield return new WaitForSeconds(cd);
        canFire = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}