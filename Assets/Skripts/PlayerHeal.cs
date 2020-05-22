using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeal : MonoBehaviour
{
    private PlayerHeart playerHeart;
    public int valueHeal;

    void Start()
    {
        playerHeart = FindObjectOfType<PlayerHeart>();
    }

    private void OnTriggerEnter2D(Collider2D heal)
    {
        if (heal.gameObject.CompareTag("Heal"))
        {
            if (playerHeart.currentHealth == playerHeart.maxHealth)
            {

            }
            else if (playerHeart.currentHealth + valueHeal >= playerHeart.maxHealth)
            {
                Destroy(heal.gameObject);
                playerHeart.Heal(playerHeart.maxHealth - playerHeart.currentHealth);
            }
            else
            {
                Destroy(heal.gameObject);
                playerHeart.Heal(valueHeal);
            }
        }
    }
}
