using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Elektro : BaseTower
{

    void Update()
    {
        CDMaterial();
        Enemie[] enemies = GetAllEnemiesInRange(towerType);
        if (canFire == true && pause == false && enemies.Length != 0) 
        {
            Zap(enemies);
            canFire = false;
            HandleCoolDown();
        }
    }

    private void Zap(Enemie[] enemieList)
    {
        for (int i = 0; i < enemieList.Length; i++)
        {
            enemieList[i].TakeDamage(damage);
        }
    }
}
