using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private int Health;
    [SerializeField] private int Gold;

    //public Text playerLevens;
    private void Update()
    {
        //Player.text = "levens: " + _currentHealth;

        RaycastHit hitInfo = new RaycastHit();
        	if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
        	{
            	print ("It's working");
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
