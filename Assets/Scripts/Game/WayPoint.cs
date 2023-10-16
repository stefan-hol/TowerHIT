using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public Vector3 GetPosition(float EnemyHeigth)
    {
        return transform.position + new Vector3(0, EnemyHeigth, 0);
    }
}
