using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;


public abstract class BaseTower : MonoBehaviour
{
    #region var/Get
    protected float radius;
    protected float cd;
    protected int damage;
    protected int level = 0;

    protected EnemyType towerType;
    protected TargetType targetType;
    private LayerMask _layer;

    [SerializeField] protected bool Target;
    [SerializeField] protected int goldCost;
    [SerializeField] protected float Height;

    protected bool canFire = true;
    protected bool pause = false;

    public void SetPause(bool _pase) { pause = _pase; }
    public int GetGoldCost() { return goldCost; }
    public bool IsTarget() { return Target; }
    public TargetType GetTarget() { return targetType; }    

    private void Start()
    {
        _layer = LayerMask.GetMask("Enemy");
        SetStats();
    }
    public virtual void SetStats(){ }
    protected void BaseStats(ScripimalPrime levels)
    {
        radius = levels.radius;
        cd = levels.cd;
        damage = levels.damage;
        towerType = levels.towerType;
        goldCost = levels.upgradeCost;
        level++;
    }
    public float GetHeight(){ return Height;}
    #endregion

    protected Enemie[] GetAllEnemiesInRange()
    {
        List<Enemie> enemiesInRange = new();

        Collider[] colls = Physics.OverlapSphere(transform.position, radius, _layer);

        if (colls.Length <= 0)
        {
            return enemiesInRange.ToArray();
        }

        foreach (Collider coll in colls)
        {
            Enemie enemie = coll.GetComponent<Enemie>();

            if (towerType == EnemyType.All || towerType == enemie.GetTyping())
            {
                enemiesInRange.Add(enemie);
            }
        }

        return enemiesInRange.ToArray();
    }

    protected Enemie GetFirstEnemyInRange()
    {
        Enemie[] enemies = GetAllEnemiesInRange();

        if (enemies.Length == 0) { return null; }   
        if (enemies.Length == 1) { return enemies[0]; }

        Enemie BestEnemie = enemies[0];
        Vector2 Best = BestEnemie.GetPathDistance();

        for(int i = 1; i < enemies.Length; i++)
        {
            Vector2 enemieDistance = enemies[i].GetPathDistance();
            if (enemieDistance.y > Best.y || (enemieDistance.x < Best.x && enemieDistance.y == Best.y))
            {
                BestEnemie = enemies[i];
                Best = enemieDistance;   
            }
        }
        return BestEnemie;
    }
    protected Enemie GetWeakestEnemie()
    {
        Enemie[] enemies = GetAllEnemiesInRange();

        if (enemies.Length == 0) { return null; }
        if (enemies.Length == 1) { return enemies[0]; }

        Enemie BestEnemie = enemies[0];

        for (int i = 1; i < enemies.Length; i++)
        {
            if (enemies[i].GetHealth() > BestEnemie.GetHealth())
            {
                BestEnemie = enemies[i];
            }
        }
        return BestEnemie;
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
    public void SetTarget(TargetType target)
    {
        targetType = target;
    }

}