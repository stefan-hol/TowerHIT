using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] BaseTower[] towers;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Tile tile = GetTile();
            if (tile == null) { return; }
            else
            {
                OpenUI(tile, 0);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Tile tile = GetTile();
            if (tile == null) { return; }
            else
            {
                OpenUI(tile, 1);
            }
        }
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
    private void OpenUI(Tile tile, int klik)
    {
        if(tile.GetIsBuildabale() == true) 
        {
            //fix dat je torens kan zien aankiezen en neerzetten
            player.SetGold(-200);
            Instantiate(towers[klik], tile.transform.position + new Vector3(0, 1, 0), transform.rotation);
            tile.SetBuildabale(false);
        }
        else 
        {
            //fix dat je de torens kan upgraden of verkopen.        
        }
    }
}
