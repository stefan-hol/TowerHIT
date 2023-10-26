using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private bool IsBuildabale;
    [SerializeField] private BaseTower tower;
    public bool GetIsBuildabale(){ return IsBuildabale; }
    public void SetTower(BaseTower _tower) {  IsBuildabale = false; tower = _tower; }
    public void Sell() {IsBuildabale = true; tower = null; }
    public BaseTower GetTower() { return tower; }
}
