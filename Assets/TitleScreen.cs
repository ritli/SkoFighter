using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
	public Image fade;

	bool fadeIn = true;

	public string levelToLoad;


    void Update()
    {
		if (Input.anyKey)
		{
			fadeIn = false;
		}

		float toAlpha = fadeIn ? 0 : 1;

		Color c = fade.color;
		c.a = Mathf.Lerp(c.a, toAlpha, Time.deltaTime * 5);

		fade.color = c;

		if (!fadeIn)
		{
			if (Mathf.Approximately(c.a,1))
			{
				SceneManager.LoadScene(levelToLoad);
			}
		}
	}
}
