using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static Unity.PlasticSCM.Editor.WebApi.CredentialsResponse;

public class MachineGun : BaseTower
{
    [SerializeField] protected BaseTowerCrip[] levels;
    public override void SetStats()
    {
        if (level > levels.Length - 1) { return; }
        radius = levels[level].radius;
        cd = levels[level].cd;
        damage = levels[level].damage;
        towerType = levels[level].towerType;
        goldCost = levels[level].upgradeCost;
        level++;
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
