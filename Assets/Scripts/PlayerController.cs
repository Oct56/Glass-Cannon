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
	[SerializeField] private float shootCooldown = 0.5f;
	private float currentCooldown;

	// SerializeField shows the variables in the inspector on Unity which is helpful for debugging.
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private float health = 12f;
	[SerializeField] private float speed = 500f;


	// Update is called once per frame
	void Update()
	{
		horizontal = Input.GetAxisRaw("Horizontal"); //Returns -1 if the input is A and 1 if the input is D 
		rb.velocity = new Vector2(horizontal * speed, rb.velocity.y); // Moves the player on the x-axis by taking a keyboard input and multipying it by the speed variable.

		if (Input.GetKeyDown(KeyCode.J) && currentCooldown <= 0) //If J is pressed while shooting isn't on cooldown...
		{
			currentCooldown = shootCooldown;
			PlayerProjectile bullet = Instantiate(projectilePrefab, fireOffset.position, transform.rotation); //Spawns a bullet prefab using the fire offset object's position and player character's rotation.
		}

		else
		{
			currentCooldown -= Time.deltaTime;
		}

		Flip();
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
