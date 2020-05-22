using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Static")]
    public Animator animatior;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public LayerMask enemyLayersTwo;


    [Header("Range and damage")]
    public float attackRange = 0.5f;
    public int attackDamage = 110;

    [Header("Attack speed")]
    public float attackSpeed = 2f;
    float nextAttackTime = 0f;

   public bool attack=false;

    void Update()
    {
        Attack();
    }

   

        //////////Attack///////////
        public void Attack()
    {
        if (Time.time >= nextAttackTime)
        {           
            if (Input.GetKeyDown(KeyCode.Q))
            {
                nextAttackTime = Time.time + 1f / attackSpeed;

                animatior.SetTrigger("Attack");

                if (Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayers))
                {

                    foreach (Collider2D enemy in Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers))
                    {
                        enemy.GetComponent<Enemy>().TakeDamage(attackDamage);

                        Debug.Log("We hit" + enemy.name);
                    }
                } else if (Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayersTwo))
                {

                    foreach (Collider2D enemy in Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayersTwo))
                    {
                        enemy.GetComponent<Enemy2>().TakeDamage(attackDamage);

                        Debug.Log("We hit" + enemy.name);
                    }
                }
            }
        }
    }


        //////////OnDrawGizmosSelected///////////
        void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

