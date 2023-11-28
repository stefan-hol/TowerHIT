using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MultiHitFollowBullet : Bullet
{
    private Explosion lit;
    // Update is called once per frame
    void Update()
    {
            transform.Translate(speed * Time.deltaTime * Vector3.forward);
            if (enemie.GetHealth() <= 0 || enemie == null) { Tick(); return; }
            transform.LookAt(enemie.transform.position);
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")) { lit.GameObject().SetActive(true); }
    }
    public override void ResetBullet()
    {
        base.ResetBullet();
        lit.GameObject().SetActive(false);
    }
    public override void SetDamage(int _damage)
    {
        lit = this.gameObject.transform.GetChild(0).GetComponent<Explosion>();
        lit.SetDamage(damage);
        lit.GameObject().SetActive(false);
    }
}
