using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    void OnDrawGizmosSelected()
    {
        if (checkPlayer == null) return;
        Gizmos.DrawWireSphere(checkPlayer.position, checkRadius);
    }

    public Transform checkPlayer;
    public float checkRadius = .2f;
    public LayerMask playerLayers;
    public int valueDamage=200;

    private PlayerHeart playerHeart;

    // Start is called before the first frame update
    void Start()
    {
        playerHeart = FindObjectOfType<PlayerHeart>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapCircle(checkPlayer.position, checkRadius, playerLayers))
        {
            playerHeart.TakeDamage(valueDamage);

        }
    }
}
