using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    protected float speed = 0;
    protected int damage;
    protected float timer;
    protected float _timer;
    protected Enemie enemie = null;

    protected virtual void Start()
    {
        _timer = 2;
        speed = 110;
    }
    public void SetTarget(Enemie _enemie)
    {
        enemie = _enemie;
    }
    public void SetDestination(Vector3 enemie, float Timer)
    {
        transform.LookAt(enemie);
        speed = Vector3.Distance(enemie, transform.position) / Timer;
        timer = Timer + 0.4f;
    }
    protected void Tick()
    {
        timer -= Time.deltaTime; 

        if (timer <= 0) { gameObject.SetActive(false); }
    }
    public virtual void ResetBullet()
    {
        timer = _timer;
    }
    public virtual void SetDamage(int _damage)
    {
        damage = _damage;
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy")) { other.GetComponent<Enemie>().TakeDamage(damage); this.GameObject().SetActive(false); }
    }
}
