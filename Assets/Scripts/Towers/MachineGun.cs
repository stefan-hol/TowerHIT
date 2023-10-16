using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.PlasticSCM.Editor.WebApi.CredentialsResponse;

public class MachineGun : BaseTower
{
    void Update()
    {
        Enemie enemie = GetFirstEnemyInRange(towerType);
        if (canFire == true && enemie != null)
        {
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
