using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Elektro : BaseTower
{
    [SerializeField] private Material oncd;
    [SerializeField] private Material normal;
    void Update()
    {
        if (canFire == false)
        {
            GetComponent<MeshRenderer>().material = oncd;
        }
        else
        {
            GetComponent<MeshRenderer>().material = normal;
        }
        Enemie[] enemies = GetAllEnemiesInRange(towerType);
        if (canFire == true && enemies.Length != 0) 
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
