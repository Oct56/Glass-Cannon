using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
	[SerializeField] private float health = 100f;
	public GameObject boss;

	// Update is called once per frame
	void Update()
    {
        if(health == 0)
        {
			boss.SetActive(false);
        }
    }

	public void TakeDamage(int damage)
	{
		health -= damage;
	}
}
