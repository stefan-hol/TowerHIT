using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MachineGun : BaseTower
{
    [SerializeField] protected BaseCrip[] levels;
    public override void SetStats()
    {
        if (level > levels.Length - 1) { return; }
        BaseStats(levels[level]);
    }
    void Update()
    {
        Enemie enemie = null;
        if (canFire == true && pause == false)
        {
            switch (targetType)
            {
                case TargetType.First: enemie = GetFirstEnemyInRange();
                    break;
                case TargetType.Weakest: enemie = GetWeakestEnemie();
                    break;
            }

            if (enemie == null || enemie.GetHealth() <= 0) {return; }
            Shoot(enemie);
            canFire = false;
            HandleCoolDown();
        }
    }
 
    private void Shoot(Enemie enemie)
    {
        enemie.TakeDamage(damage);
    }
}
