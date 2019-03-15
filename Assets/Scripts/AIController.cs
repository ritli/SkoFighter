using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller 
{
	PlayerController player;

	public float range = 3f; 

    void Start()
    {
		player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
		float move = 0;

		if (Vector2.Distance(transform.position, player.transform.position) > range)
		{
			move = Mathf.Sign(player.transform.position.x - transform.position.x);
		}

		MoveInput(move);
	}
}
