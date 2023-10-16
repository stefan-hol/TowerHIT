using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private int Health;
    [SerializeField] private int Gold;

    //public Text playerLevens;
    private void Update()
    {
        //Player.text = "levens: " + _currentHealth;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.CompareTag("Tile"))
            {
                Tile tile = hitInfo.transform.GetComponent<Tile>();
                print(tile.GetIsBuildabale());
                if (tile.GetIsBuildabale() == true)
                {
                    // open tower menu to place
                }
                else if (tile.GetIsBuildabale() == false)
                {
                    // open tower upgrade dingetje
                }
            }
        }    	
    }
    void Death()
    {
        SceneManager.LoadScene(0);
    }
    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Death();
        }
    }
    public void Kill(int _gold)
    {
        Gold += _gold;
    }
}
