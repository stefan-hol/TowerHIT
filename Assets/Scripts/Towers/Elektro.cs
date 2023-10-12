using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Elektro : BaseTower
{

    [SerializeField] private int damage;
    void Update()
    {
        if (GetAllEnemiesInRange(towerType).Length != 0) {Zap(GetAllEnemiesInRange(towerType));}
    }

    private void Zap(Enemie[] enemieList)
    {
        for (int i = 0; i < enemieList.Length; i++)
        {
            enemieList[i].TakeDamage(damage);
        }
    }

}
