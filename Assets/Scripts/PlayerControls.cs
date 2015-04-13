﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PlayerControls : MonoBehaviour {
    public float maxHealth = 100f;
    public float maxHunger = 100f;
    public float Health = 100f;
    public float Hunger = 100f;

	public float baseSpeed          = 1f;
	public float jumpAcceleration   = 400f;
	public float hungerRaiseRate    = 0.2f;
	public int attackDamage         = 10;
	public float attackCooldown     = 0.2f;
    private float groundRadius      = 0.3f;

    public Transform floorCheckCollider;
    public LayerMask whatIsFloor;
    public Slider HealthBar;
    public Slider HungerBar;
    public Collider2D attackCollider; 

	private Rigidbody2D rigidBody2D;
    private bool isOnFloor = false;
    private bool isLookingRight = true;
	
	private float attackTimer;
	private Animator animator;    

	// Use this for initialization
    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        HealthBar.maxValue = maxHealth;
        HungerBar.maxValue = maxHunger;
        HealthBar.value = maxHealth;
        HungerBar.value = maxHunger;
    }

    // Update is called once per frame
	void Update () 
	{
        if (!Global.isPaused())
        {
            HUD();
        }
	}

    void FixedUpdate() 
    {
        if (!Global.isPaused())
        {
            floorCheck();
            Attack();
            Movement();
        }
    }


	float getSpeed()
	{
        if (Input.GetButton("Run"))
		{
			return baseSpeed * 0.2f;
		}
		
		return baseSpeed * 0.1f;	
	}

    void floorCheck() 
    {
        isOnFloor = Physics2D.OverlapCircle(floorCheckCollider.position, groundRadius, whatIsFloor); 
    }

	void Movement ()
	{
        ////////////////ВЛЕВО/ВПРАВО///////////////
        float forwardMovement = Input.GetAxis("Horizontal") ; 		
		float speed = forwardMovement * getSpeed();

        if (isOnFloor)//Ходим только по полу
        {
            rigidBody2D.velocity = new Vector2(speed, rigidBody2D.velocity.y);
        }

        ////////////////ПРЫЖОК/////////////////////
		if(Input.GetButtonDown("Jump") && isOnFloor)
		{
		    rigidBody2D.AddForce(new Vector2(0f, jumpAcceleration));  
		}

        ////////////////АНИМАЦИЯ///////////////////
        if ((forwardMovement > 0 && !isLookingRight) || (forwardMovement < 0 && isLookingRight)) { TurnAnimation(); }
        //animator.SetBool("walking", speed != 0);
	}

    void TurnAnimation()
    {
        isLookingRight = !isLookingRight;
        //Quaternion tmpRotation = transform.rotation;
        //tmpRotation.y = isLookingRight ? 0:180;
        //transform.rotation = tmpRotation;

        Vector3 turnVector = transform.localScale;
        turnVector.x *= -1;
        transform.localScale = turnVector;
    }

    void HUD()
    {
        HealthBar.value     = Health;
        HungerBar.maxValue  = Hunger;
    }
    

	void Attack()
	{
	/*	if(attackTimer < 0)
			attackTimer = 0;

		
		if(attackTimer == 0)
		{
			bool attacking = Input.GetButtonDown("Attack1");
			if(attacking)	
			{
				attackCollider.enabled = true;
				attackTimer = attackCooldown;			
			}
		}
		else if(attackTimer > 0)
		{
			attackCollider.enabled = false;
			attackTimer -= Time.deltaTime;
		}*/
	}

    //void OnCo
}
