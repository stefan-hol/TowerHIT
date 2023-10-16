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
                OpenUI(tile);
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
    private void OpenUI(Tile tile)
    {
        if(tile.GetIsBuildabale() == true) 
        {
            //fix dat je torens kan zien aankiezen en neerzetten
            player.SetGold(-200);
            Instantiate(towers[0], tile.transform.position + new Vector3(0, 1, 0), transform.rotation);
            tile.SetBuildabale(false);
        }
        else 
        {
            //fix dat je de torens kan upgraden of verkopen.        
        }
    }
}
