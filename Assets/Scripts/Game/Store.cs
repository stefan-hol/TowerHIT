using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Store : MonoBehaviour
{
# region variables/GetSeters/Pause
    [SerializeField] Player player;
    [SerializeField] BaseTower[] towers;

    [SerializeField] GameObject buybuttons;
    [SerializeField] GameObject alreadybuttons;
    [SerializeField] GameObject Targetbuttons;

    private List<Tile> towerTiles = new();
    Tile tile;

    public TMP_Dropdown dropdown;

    private Tile GetTile()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.CompareTag("Tile"))
        {
            return hitInfo.transform.GetComponent<Tile>();
        }
        return null;
    }
    void PauseGame()
    {
        Time.timeScale = 0;
        foreach (Tile tile in towerTiles) { tile.GetTower().SetPause(true); }
    }
    private void Update()
    {
        OpenUI();
    }
    #endregion
    private void OpenUI()
    {
        if (buybuttons.activeSelf == false && alreadybuttons.activeSelf == false)
        {
            if (Input.GetMouseButtonDown(0) && GetTile() != null)
            {
                PauseGame();
                tile = GetTile();
                if (tile.GetIsBuildabale()) { buybuttons.SetActive(true); }
                else 
                { 
                    alreadybuttons.SetActive(true);
                    if (tile.GetTower().IsTarget() == true) 
                    {
                        Targetbuttons.SetActive(true);
                        dropdown.value = (int)tile.GetTower().GetTarget();
                    }
                }

            }
        }
    }
    public void UpgradeTower()
    {
        BaseTower tower = tile.GetTower();
        if (player.GetGold() > tower.GetGoldCost()) { tile.GetTower().SetStats(); player.SetGold(-tower.GetGoldCost()); }
        Exit();
    }
    public void SetTower(BaseTower tower)
    {  
        if(player.GetGold() >= tower.GetGoldCost())
        {
            player.SetGold(-tower.GetGoldCost());
            BaseTower cloneTower;
            cloneTower = Instantiate(tower, tile.transform.position + new Vector3(0, 1, 0), transform.rotation);
            tile.SetTower(cloneTower);
            towerTiles.Add(tile);
        }
        Exit();
    }
    public void SetTarget(int val)
    {
       switch (val) {
            case 0:
                tile.GetTower().SetTarget(TargetType.First);
                break;
            case 1:
                tile.GetTower().SetTarget(TargetType.Weakest);
                break;
        }
    }
    public void Sell()
    {
        BaseTower tower = tile.GetTower();
        player.SetGold(/*nog wat op verzinnen*/100);
        tile.Sell();
        towerTiles.Remove(tile);
        Destroy(tower.gameObject);
        Exit();
    }
    public void Exit()
    {
        Time.timeScale = 1;
        foreach (Tile tile in towerTiles) { tile.GetTower().SetPause(false); }
        buybuttons.SetActive(false);
        alreadybuttons.SetActive(false);
        Targetbuttons.SetActive(false); 
    }
}