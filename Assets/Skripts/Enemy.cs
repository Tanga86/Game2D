using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



////////enum/////////
enum MoveState
{
    Live,
    Dead
}

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform checkCeiling;
    [SerializeField] private Transform checkHand;


    [Header("State")]
    public Transform player;
    public GameObject enemy;
    public Animator animator;

    [Header("Agro and Walk")]
    public float agroRange;
    public float moveSpeed;
    public float _walkTime;
    public int valueDamage;

    [Header("Healt Point")]
    public int maxHealth = 100;
    public int currentHealth;


    private bool facingRight = true;
    private Rigidbody2D rb;
    private GameObject playerMain;
    private MoveState _moveState = MoveState.Live;

    private PlayerHeart playerHeart;
    private ScoreManager scoreManager;
    private Teleport teleport;

    void Start()
    {

        scoreManager = FindObjectOfType<ScoreManager>();
        playerHeart = FindObjectOfType<PlayerHeart>();
        teleport = FindObjectOfType<Teleport>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerMain = GameObject.FindGameObjectWithTag("Player");
        
    }

    void FixedUpdate()
    {
        float p = playerMain.transform.position.x;
        float e = gameObject.transform.position.x;
        float distance = Vector2.Distance(transform.position, player.position);
        float distanceCeiling = Vector2.Distance(transform.position, checkCeiling.position);
        float distanceHand = Vector2.Distance(transform.position, checkHand.position);

        //print(distanceCeiling);

        if ((distance < agroRange) &&(_moveState == MoveState.Live))
        {
            ChasePlayer();
            
        }
        else if(_moveState == MoveState.Live)
        {
           StopChasePlayer();
        }

        if ((distanceCeiling < 0.35f || distanceHand < 0.20f 
            || distanceHand < 0.20f) && (_moveState == MoveState.Live))
        {
            playerHeart.TakeDamage(valueDamage);
            Die();
        }

        if (e-p > 0 && !facingRight)
        {
            //  Меняем направление.
            Swap();
        }
        // В противном случае, если input перемещает игрока влево, 
        //  а игрок направлен вправо ...
        else if (e - p < 0 && facingRight)
        {
            //  Меняем направление.
            Swap();
        }
    }

    //Меняем направление
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
            Die();
        }
    }
 
    ////////Die//////////
    void Die()
    {
        teleport.kills++;
        scoreManager.ChangeScoreKill();
        _moveState = MoveState.Dead;
        rb.velocity = new Vector2(0, 0);
        animator.Play("Dead");
        Destroy(enemy, _walkTime);
       this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0f;
    }





    ////////StopChasePlayer//////////
    void StopChasePlayer()
    {
        rb.velocity =new Vector2(0,0);// new Vector2.zero
        animator.Play("Idle");
    }

    ////////ChasePlayer//////////
    void ChasePlayer()
    {
        animator.Play("Walk");
        if (transform.position.x < player.position.x)
        {
            rb.velocity = new Vector2(moveSpeed, 0);          
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, 0);            
        }
    }
}
