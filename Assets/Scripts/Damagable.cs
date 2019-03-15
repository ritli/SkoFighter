using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
	public int health, maxHealth;

	public void DealDamage(int damage)
	{
		health -= damage;

		GetComponentInChildren<SpriteAnimator>()?.FlashSpriteInColor(Color.red, 0.6f);
	}

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
