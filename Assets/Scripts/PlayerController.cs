using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private float horizontal;
	public PlayerProjectile projectilePrefab;
	public Transform fireOffset;

	private bool isFacingRight = true;

	private float currentCooldown;

	public Vector2 standingSize;
	public Vector2 crouchingSize;
	public Vector2 offsetSize;

	public BoxCollider2D Collider;

	// SerializeField shows the variables in the inspector on Unity which is helpful for debugging.
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private float health = 12f;
	[SerializeField] private float speed = 500f;

	[SerializeField] public float groundRadius;
	[SerializeField] private LayerMask ground;
	[SerializeField] public bool grounded = true;
	[SerializeField] private float jumpForce;

	[SerializeField] private float shootCooldown = 0.5f;


	void Start()
	{
		Collider = GetComponent<BoxCollider2D>();
		standingSize = Collider.size;
		Collider.size = standingSize;
	}

	// Update is called once per frame
	void Update()
	{
		//Movement
		horizontal = Input.GetAxisRaw("Horizontal"); //Returns -1 if the input is A and 1 if the input is D 
		rb.velocity = new Vector2(horizontal * speed, rb.velocity.y); // Moves the player on the x-axis by taking a keyboard input and multipying it by the speed variable.

		//Shooting
		if (Input.GetKeyDown(KeyCode.J) && currentCooldown <= 0) //If J is pressed while shooting isn't on cooldown...
		{
			currentCooldown = shootCooldown;
			PlayerProjectile bullet = Instantiate(projectilePrefab, fireOffset.position, transform.rotation); //Spawns a bullet prefab using the fire offset object's position and player character's rotation.
		}

		else
		{
			currentCooldown -= Time.deltaTime;
		}

		//Jumping
		grounded = Physics2D.OverlapCircle(rb.position, groundRadius, ground); //Draws a circle around the player taking in the player's position and ground radius as input. The circle checks to see if a LayerMask of type ground is touching it.

		if (Input.GetButtonDown("Jump") && grounded)
		{
			Jump();
		}

		//Crouching
		if (Input.GetKeyDown(KeyCode.S)) //Crouches when S button is held
		{
			Collider.size = crouchingSize;
			Collider.offset = offsetSize;
		}

		if (Input.GetKeyUp(KeyCode.S)) //Uncrouches when S is no longer held who would have thought.
		{
			Collider.size = standingSize;
			Collider.offset = new Vector2(0, 0);
		}

		Flip();
	}

	private void Jump()
	{
		grounded = false;
		rb.velocity = new Vector2(rb.velocity.x, jumpForce);
	}

	private void Flip()
	{
		if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) // Rotates the character's rotation by 180 degrees based on if the character is looking left or right
		{
			isFacingRight = !isFacingRight;
			transform.Rotate(0, -180f, 0);
		}
		transform.Rotate(0, 0, 0);
	}
}
