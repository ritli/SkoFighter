using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
	Color flashColor;
	float time;
	private SpriteRenderer sprite;
	private Color originalColor;

	public void FlashSpriteInColor(Color color, float duration)
	{
		time = duration;
		flashColor = color;
	}

    void Start()
    {
		sprite = GetComponent<SpriteRenderer>();
		originalColor = sprite.color;
    }

    void Update()
    {
		Color toColor = originalColor;

        if (time > 0)
		{
			toColor = Color.Lerp(toColor, flashColor, Mathf.Sin(Time.time * 20));
			time -= Time.deltaTime;
		}

		sprite.color = Color.Lerp(sprite.color, toColor, Time.deltaTime * 15);
	}
}
