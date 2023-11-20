using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Towers
{
    [SerializeField] public BaseTower[] towers;
    [HideInInspector] public List<string> names;
};

public class Store : MonoBehaviour
{
    #region variables/GetSeters/Pause
    [SerializeField] private Towers towers;
    [SerializeField] private Player player;
    [SerializeField] private TMP_Text UpgardeCost;
    [SerializeField] private GameObject buybuttons;
    [SerializeField] private GameObject alreadybuttons;
    [SerializeField] private GameObject Targetbuttons;

    private List<Tile> towerTiles = new();
    private Tile tile;

    public TMP_Dropdown dropdown;
    public TMP_Dropdown dropTowers;

    private Tile GetTile()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo) && hitInfo.transform.CompareTag("Tile"))
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
    private void Start()
    {
        foreach(BaseTower tower in towers.towers)
        {
            towers.names.Add(tower.name + " " + tower.GetGoldCost());
        }
        dropTowers.AddOptions(towers.names);
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
                if (tile.GetIsBuildabale()) {buybuttons.SetActive(true); dropTowers.value = 0; }
                else 
                {
                    UpgardeCost.text = "Upgrade: " + tile.GetTower().GetGoldCost();
                    alreadybuttons.SetActive(true);
                    if (tile.GetTower().IsTarget() == true) 
                    {
                        Targetbuttons.SetActive(true);
                        dropdown.value = (int)tile.GetTower().GetTarget();
                    }
                }

            }
        }
        if(Input.GetMouseButtonDown(1) && GetTile() != null) 
        {
            BaseTower to = GetTower();
            if(to != null) { print("jottems"); }
        }
    }
    private BaseTower GetTower()
    {
        if (Physics.Raycast(GetTile().transform.position, transform.TransformDirection(Vector3.up), out RaycastHit hitInfo) && hitInfo.transform.CompareTag("Tower"))
        {
            return hitInfo.transform.GetComponent<BaseTower>();
        }
        return null;
    }
    public void UpgradeTower()
    {
        BaseTower tower = tile.GetTower();
        if (player.GetGold() > tower.GetGoldCost()) { player.SetGold(-tower.GetGoldCost()); tile.GetTower().SetStats(); }
        Exit();
    }
    public void SetTower(BaseTower tower)
    {
        if (player.GetGold() >= tower.GetGoldCost())
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
        switch (val)
        {
            case 0:
                tile.GetTower().SetTarget(TargetType.First);
                break;
            case 1:
                tile.GetTower().SetTarget(TargetType.Weakest);
                break;
        }
    }
    public void SetDropTower(int val)
    {
        SetTower(towers.towers[val - 1]);
        dropTowers.value = 0;
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