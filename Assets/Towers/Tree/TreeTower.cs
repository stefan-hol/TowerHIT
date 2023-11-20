using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TreeTower : BaseTower
{
    private Bullet bullet;
    [SerializeField] private Towercrip[] levels;
    public override void SetStats()
    {
        if (level > levels.Length - 1) { return; }
        bullet = levels[level].bullet;
        BaseStats(levels[level]);

    }
    void Update()
    {
        Enemie enemie = null;
        if (canFire == true && pause == false)
        {
            switch (targetType)
            {
                case TargetType.First:
                    enemie = GetFirstEnemyInRange();
                    break;
                case TargetType.Weakest:
                    enemie = GetWeakestEnemie();
                    break;
            }

            if (enemie == null || enemie.GetHealth() <= 0) { return; }
            Shoot(enemie);
            canFire = false;
            HandleCoolDown();
        }

    }

    private void Shoot(Enemie enemie)
    {
        Bullet bul;

        bul = Instantiate(bullet, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        bul.SetTarget(enemie);
        bul.SetDamage(damage);
    }

}
