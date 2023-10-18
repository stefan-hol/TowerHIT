using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.PlasticSCM.Editor.WebApi.CredentialsResponse;

public class MachineGun : BaseTower
{
    void Update()
    {
        CDMaterial();
        if (canFire == true)
        {
            Enemie enemie = GetFirstEnemyInRange(towerType);
            if (enemie == null || enemie.GetHealth() <= 0) { return; }
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
