using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] BaseTower[] towers;
    [SerializeField] GameObject buttons;

    Tile tile;
    private void Update()
    {
        OpenUI();
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        foreach (BaseTower tower in towers) { tower.SetPause(true); }
    }
    void ResumeGame()
    {
        Time.timeScale = 1;
        foreach (BaseTower tower in towers) { tower.SetPause(false); }
    }

    private Tile GetTile()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.CompareTag("Tile"))
        {
            return hitInfo.transform.GetComponent<Tile>();
        }
        return null;
    }
    private void OpenUI()
    {
        if (buttons.activeSelf == false)
        {
            if (Input.GetMouseButtonDown(0) && GetTile() != null)
            {
                PauseGame();
                tile = GetTile();
                if (tile.GetIsBuildabale()) { buttons.SetActive(true); }
                else { ResumeGame();/*upgrade dingetje en toren verkopen*/ }

            }
        }
    }

    public void SetTower(BaseTower tower)
    {  
        if(player.GetGold() >= tower.GetGoldCost())
        {
            player.SetGold(-tower.GetGoldCost());
            Instantiate(tower, tile.transform.position + new Vector3(0, 1, 0), transform.rotation);
            tile.SetBuildabale(false);
        }
        ResumeGame();
        buttons.SetActive(false);   
    }
}


//if(tile.GetIsBuildabale() == true) 
//{
//    //fix dat je torens kan zien aankiezen en neerzetten
//    player.SetGold(-200);
//    Instantiate(towers[klik], tile.transform.position + new Vector3(0, 1, 0), transform.rotation);
//    tile.SetBuildabale(false);
//}
//else 
//{
//    //fix dat je de torens kan upgraden of verkopen.        
//}