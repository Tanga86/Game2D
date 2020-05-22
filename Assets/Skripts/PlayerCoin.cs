using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coin)
    {
        if (coin.gameObject.CompareTag("Coin"))
        {
            Destroy(coin.gameObject);
        }
    }
}
