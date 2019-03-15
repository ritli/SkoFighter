using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
	public void RecieveInput()
	{
		MoveInput(Input.GetAxis("Horizontal"));

		if (Input.GetButtonDown("Vertical"))
		{
			JumpInput(1);
		}
		else
		{
			JumpInput(0);
		}

		if (Input.GetButtonDown("Fire1"))
		{
			FightInput(0);
		}
		if (Input.GetButtonDown("Fire2"))
		{
			FightInput(1);
		}

	}

    void Update()
    {
		RecieveInput();
    }
}
