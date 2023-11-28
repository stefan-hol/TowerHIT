using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private int damage;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")) { 
            other.GetComponent<Enemie>().TakeDamage(damage);
        }
    }
    public void SetDamage(int _damage)
    {
        damage = _damage;
    }
}
