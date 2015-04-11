using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PlayerControls : MonoBehaviour {

	public float baseSpeed = 1f;
	public float jumpAcceleration = 400f;
	public float maxHealth = 100f;
	public float maxHunger = 100f;
	public float hungerRaiseRate = 0.2f;
	public int attackDamage = 10;
	public float attackCooldown = 0.2f;


	private Rigidbody2D rigidBody2D;
//	private bool runActive = false;
	
	private Slider healthBar;
	private Slider hungerBar;
	private Collider2D attackCollider; 
	private float attackTimer;
	private Animator animator;    

	// Use this for initialization
    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
	void Update () 
	{
        if (!Global.isPaused())
	    {
	        Movement();
	        Attack();
	    }

	}

	float getSpeed()
	{
        if (Input.GetButton("Run"))
		{
			Debug.Log("shift");
			return baseSpeed * 0.2f;
		}
		
		return baseSpeed * 0.1f;	
	}
	
	void Movement ()
	{			
		
		float forwardMovement = Input.GetAxis("Horizontal") ; 		
		float speed = forwardMovement * getSpeed();
	
		rigidBody2D.velocity = new Vector2(speed, rigidBody2D.velocity.y);
		
		
		if( forwardMovement > 0)
		{
			Quaternion tmpRotation = transform.rotation;
			tmpRotation.y = 0;
			transform.rotation = tmpRotation;
			//animator.SetBool("walking", true);
		}
		else if(forwardMovement < 0)
		{
			Quaternion tmpRotation = transform.rotation;
			tmpRotation.y = 180;
			transform.rotation = tmpRotation;
			//animator.SetBool("walking", true);
		}
		else
		{
			//animator.SetBool("walking", false);
		}
	
		
		
		if(Input.GetButtonDown("Jump"))
		{
			rigidBody2D.AddForce(new Vector2(0f, jumpAcceleration));  
 		   if(rigidBody2D.velocity.y > 1)
           {
               rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, 1);
           }
		}
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
}
