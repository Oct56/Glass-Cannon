using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
	public float speed = 8f;
	public Rigidbody2D rb;

	// Update is called once per frame
	void Update()
    {
		transform.position += transform.right * Time.deltaTime * speed; //Shoots the projectile forward based on player position.
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if( collision.transform.tag == "Enemy")
		{
			collision.gameObject.GetComponent<BossController>().TakeDamage(1);
		}
		Destroy(gameObject);
	}
}
