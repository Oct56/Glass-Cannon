using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
	[SerializeField] private float health = 100f;


	// Update is called once per frame
	void Update()
    {
        
    }

	public void TakeDamage(int damage)
	{
		health -= damage;
	}
}
