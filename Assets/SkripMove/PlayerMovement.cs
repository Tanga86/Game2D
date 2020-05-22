using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public MainMoveLogic controller;
	public Animator animator;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	public bool jump = false;
	bool crouch = false;
    private bool attack = false;
    private float timeLeft= 0.35f;

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;

            animator.SetBool("IsJumping", true);
        }


        if (attack == true)
        {
               horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed * 0.4f;

        }
        else if (attack == false && controller.isGroud == true)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        }
        else if (controller.isGroud == false)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed * 0.8f;
        }
        else
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        }
		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));



        if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		} else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}
    }

    public void OnLanding ()
	{
		animator.SetBool("IsJumping", false);
	}

	public void OnCrouching (bool isCrouching)
	{
		animator.SetBool("IsCrouching", isCrouching);
	}

    void OnAttack()
    {
        animator.SetBool("AttackBool", true);

        attack = true;
    }
    void OffAttack()
    {
        animator.SetBool("AttackBool", false);
        attack = false;
    }


    void FixedUpdate ()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            attack = false;
            timeLeft = 0.35f;
        }
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
        
    }
}
