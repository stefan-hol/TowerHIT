using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TreeTower : BaseTower
{
    private BulletPool _bulletPool;
    private Animator anim;
    private float timer;
    [SerializeField] private Towercrip[] levels;
    [SerializeField] private GameObject BulletHole;
    [SerializeField] private GameObject Canon;
    public override void SetStats()
    {
        _bulletPool = GetComponent<BulletPool>();
        if (level > levels.Length - 1) { return; }
        timer = levels[level].timer;
        _bulletPool.SetDamage(levels[level].damage);
        _bulletPool.InitializePool(levels[level].bullet);
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
        //anim.Play("Base Layer.CanonShoot");
        Canon.transform.LookAt(enemie.transform.position);
        Bullet bul = _bulletPool.GetBullet();
        bul.transform.position = BulletHole.transform.position;
        bul.transform.LookAt(enemie.transform.position);
        bul.SetTarget(enemie);
    }
}
