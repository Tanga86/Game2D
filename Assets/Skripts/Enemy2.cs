using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [Header("Speed")]
    public float moveSpeed;
    public float moveSpeedBack;

    [Header("Distance")]
    public float agroRange1;
    public float agroRange2;
    public float retreartdist;

    [Header("FireBall")]
    public float startimeBtw;
    private float timeBtw;
    public GameObject ball;

    [Header("Healt Point")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Stop")]
    public Transform checkStop;
    public float checkRadius = .2f;
    public LayerMask stopLayers;

    private Transform player;
    private bool facingRight = true;
    private ScoreManager scoreManager;
    private Teleport teleport;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        teleport = FindObjectOfType<Teleport>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtw = startimeBtw;
        currentHealth = maxHealth;

    }

    void OnDrawGizmosSelected()
    {
        if (checkStop == null) return;
        Gizmos.DrawWireSphere(checkStop.position, checkRadius);
    }

 
    void Update()
    {
        float p = player.transform.position.x;
        float e = gameObject.transform.position.x;
        float distance = Vector2.Distance(transform.position, player.position);
      //  print(distance);
        if (distance < agroRange1)
        {
                 if (distance > agroRange2)
            {
                if (Physics2D.OverlapCircle(checkStop.position, checkRadius, stopLayers) == true)
                {
                    transform.position = this.transform.position;
                } else
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }
            else if (distance < agroRange2 && distance > retreartdist)
            {
                transform.position = this.transform.position;
            }
            else if (distance < retreartdist)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -moveSpeedBack * Time.deltaTime);
            }

            if (timeBtw <= 0)
            {
                Instantiate(ball, transform.position, Quaternion.identity);
                timeBtw = startimeBtw;
            }
            else
            {
                timeBtw -= Time.deltaTime;
            }

            if (e - p > 0 && !facingRight)
            {
                Swap();
            }

            else if (e - p < 0 && facingRight)
            {
                Swap();
            }

        }    
    }

    private void Swap()
    {
        // Переключить переменную направления лица игрока
        facingRight = !facingRight;

        // Разворачиваем его
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    ////////TakeDamage//////////
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            teleport.kills++;
            scoreManager.ChangeScoreKill();
            Destroy(gameObject);

        }
    }




}
