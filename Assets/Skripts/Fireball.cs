using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed;
    private Transform player;

    private Vector2 target;
    private PlayerHeart playerHeart;

    public float timeLeft;
    public Transform attackPoint;
    public LayerMask PlayerLayers;
    public LayerMask GroundLayers;
    public float attackRange = 0.5f;
    public int valueDamage;

    //////////OnDrawGizmosSelected///////////
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


    void Start()
    {
        playerHeart = FindObjectOfType<PlayerHeart>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            DestroyBall();
        }

        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        if (Physics2D.OverlapCircle(attackPoint.position, attackRange, PlayerLayers))
        {
            playerHeart.TakeDamage(valueDamage);

            DestroyBall();
        }
        else if (Physics2D.OverlapCircle(attackPoint.position, attackRange, GroundLayers))
        {
            DestroyBall();
        }
    }

    

    private void DestroyBall()
    {
        Destroy(gameObject);
    }
}
