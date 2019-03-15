using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
	public int health, maxHealth;

	Fighter fighter;

	public void DealDamage(int damage)
	{
		health -= damage;

		GetComponentInChildren<SpriteAnimator>()?.FlashSpriteInColor(Color.red, 0.6f);

		fighter?.GetHit();
	}	

    void Start()
    {
		fighter = GetComponent<Fighter>();
		health = maxHealth;
    }
}
