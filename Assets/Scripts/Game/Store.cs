using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    private BaseTower tower;

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
    private BaseTower GetTower()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit jottems) && jottems.transform.CompareTag("Tower"))
        {
            return jottems.transform.GetComponent<BaseTower>();
        }
        if (Physics.Raycast(GetTile().transform.position, transform.TransformDirection(Vector3.up), out RaycastHit hitInfo) && hitInfo.transform.CompareTag("Tower"))
        {
            return hitInfo.transform.GetComponent<BaseTower>();
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
            if (Input.GetMouseButtonDown(0) && (GetTile() != null || GetTower() != null))
            {
                PauseGame();
                tile = GetTile();
                tower = GetTower();
                if (tower == null) {buybuttons.SetActive(true); dropTowers.value = 0; }
                else 
                {
                    UpgardeCost.text = "Upgrade: " + tower.GetGoldCost();
                    alreadybuttons.SetActive(true);
                    if (tower.IsTarget() == true) 
                    {
                        Targetbuttons.SetActive(true);
                        dropdown.value = (int)tower.GetTarget();
                    }
                }

            }
        }
    }
    
    public void UpgradeTower()
    {
        
        if (player.GetGold() > tower.GetGoldCost()) { player.SetGold(-tower.GetGoldCost()); tower.SetStats(); }
        Exit();
    }
    public void SetTower(BaseTower _tower)
    {
        if (player.GetGold() >= _tower.GetGoldCost())
        {
            player.SetGold(-_tower.GetGoldCost());
            BaseTower cloneTower;
            cloneTower = Instantiate(_tower, tile.transform.position + new Vector3(0, _tower.GetHeight(), 0), Quaternion.identity);
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
                tower.SetTarget(TargetType.First);
                break;
            case 1:
                tower.SetTarget(TargetType.Weakest);
                break;
        }
    }
    public void SetDropTower(int val)
    {
        if (val == 0) { return; }
        SetTower(towers.towers[val - 1]);
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