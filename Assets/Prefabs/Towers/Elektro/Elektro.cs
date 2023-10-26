using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class Elektro : BaseTower
{
    [SerializeField] protected ElektroCrip[] leveling;
    private float _slow;

    public override void SetStats()
    {
        if (level > leveling.Length - 1) { return; }
        radius = leveling[level].radius;
        cd = leveling[level].cd;
        damage = leveling[level].damage;
        towerType = leveling[level].towerType;
        goldCost = leveling[level].upgradeCost; 
        _slow = leveling[level].slow;
        level++;
    }
    void Update()
    {
        Enemie[] enemies = GetAllEnemiesInRange();
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
            enemieList[i].SetSpeed(_slow);
        }
    }
}
