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
