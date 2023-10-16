using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private bool IsBuildabale;
    public bool GetIsBuildabale()
    {
        return IsBuildabale;
    }
}
