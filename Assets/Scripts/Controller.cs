using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	public Fighter fighter;

	public void MoveInput(float direction)
	{
		fighter.xVelocity = direction;
	}

	public void JumpInput(int jumping)
	{
		if (fighter.jump < 1)
		{
			fighter.jump = jumping;
		}
	}

	public void FightInput(int index)
	{
		switch (index)
		{
			case 0:
				fighter.StartPunch();
				break;
			case 1:
				fighter.StartKick();
				break;
			case 2:
				break;

			default:
				break;
		}
	}
}
