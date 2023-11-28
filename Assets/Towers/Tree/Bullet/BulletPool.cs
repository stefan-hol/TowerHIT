using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private List<Bullet> bulletPool;
    private int damage;
    public void InitializePool(Bullet bulletPrefab)
    {
        if (bulletPool.Count != 0) { 
            foreach (Bullet bul in bulletPool){Destroy(bul.GameObject());}
        }

        bulletPool = new List<Bullet>();
        int poolSize = 5;
        for (int i = 0; i < poolSize; i++)
        {
            Bullet bullet = Instantiate(bulletPrefab);
            bullet.SetDamage(damage);
            bullet.GameObject().SetActive(false);
            bullet.transform.parent = transform;
            bulletPool.Add(bullet);
        }
    }
    public void SetDamage(int _damage)
    {
        damage = _damage;  
    }

    public Bullet GetBullet()
    {
        foreach (Bullet bullet in bulletPool)
        {
            if (!bullet.GameObject().activeInHierarchy)
            {
                bullet.ResetBullet();
                bullet.GameObject().SetActive(true);
                return bullet;
            }
        }
        return null;
    }
}
