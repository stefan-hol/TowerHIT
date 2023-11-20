using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]private Enemie enemie;
    [SerializeField] private int speed;
    private bool lit = false;
    private float timer = 4;
    private int damage;
    private void Update()
    {
        Tick();
        if (!lit)
        {
            transform.LookAt(enemie.transform.position);
        }
        transform.Translate(speed * Time.deltaTime * Vector3.forward);

    }
    public void SetTarget(Enemie _enemie)
    {
        enemie = _enemie;
    }
    private void Tick()
    {
        if (lit) { timer -= Time.deltaTime; }
        else
        {
            if (enemie == null) { lit = true; }
        }
        if(timer <= 0) { Destroy(gameObject); }
    }
    public void SetDamage(int _damage)
    {
        damage = _damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy")) { print("lit"); enemie.TakeDamage(damage); Destroy(gameObject); }
    }
}
