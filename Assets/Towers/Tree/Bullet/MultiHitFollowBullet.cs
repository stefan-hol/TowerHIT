using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class MultiHitFollowBullet : Bullet
{
    [SerializeField] private int radius;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
        if (enemie.GetHealth() <= 0 || enemie == null) { Tick(); return; }
        transform.LookAt(enemie.transform.position);
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")) { Explo(); this.GameObject().SetActive(false); }
    }
    private void Explo()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Enemy"));
        foreach (Collider col in objectsInRange)
        {
            if (col.TryGetComponent<Enemie>(out var enemy))
            {
                // linear falloff of effect
                float proximity = (transform.position - enemy.transform.position).magnitude;
                float effect = 1 - (proximity / (radius + 0.5f));
                if (effect < 0 || effect > 1) 
                { 
                    print(Mathf.RoundToInt(damage * effect)); 
                }
                enemy.TakeDamage(Mathf.RoundToInt(damage * effect));
            }
        }
    }
}
