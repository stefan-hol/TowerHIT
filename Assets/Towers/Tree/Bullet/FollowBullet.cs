using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowBullet : Bullet
{
    private bool hit = false;
    // Start is called before the first frame update
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
        if (enemie.GetHealth() <= 0 || enemie == null) { Tick(); return; }
        transform.LookAt(enemie.transform.position);
    }
    public override void ResetBullet()
    {
        base.ResetBullet();
        hit = false;
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")&& hit == false) { other.GetComponent<Enemie>().TakeDamage(damage); this.GameObject().SetActive(false); hit = true; }
    }
}
