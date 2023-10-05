using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] private float EnemyHeigth;
    public Vector3 GetPosition()
    {
        return transform.position + new Vector3(0, 0.5f, 0);
    }
}
