using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeart : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    private GameObject player;

    public HeartBart heartBart;

    public GameObject pauseMenuUI;



    void Start()
    {
        //  HeartBart heartBart = new HeartBart();
        player= GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth;
        heartBart.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth<=0)
        {
            Destroy(player);
            DeadMenu();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        heartBart.SetHealth(currentHealth);
        
    }

    public void Heal(int valueHeal)
    {
        currentHealth += valueHeal;
        heartBart.SetHealth(currentHealth);

    }

    private void DeadMenu()
    {
        pauseMenuUI.SetActive(true);
    }

}
