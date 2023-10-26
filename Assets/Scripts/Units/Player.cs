using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private int Health;
    [SerializeField] private int Gold;
    [SerializeField] private TMP_Text UI;

    public void SetGold(int _gold) { Gold += _gold; }
    public int GetGold() { return Gold; } 

    //public Text playerLevens;
    private void Update()
    {
        UI.text = "HP: " + Health + " Gold: " + Gold;
   	
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
}
