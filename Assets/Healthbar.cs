using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
	Slider slider;
	float toValue; 

	public float Value
	{
		set
		{
			toValue = value;
		}
	}

	private void Start()
	{
		slider = GetComponent<Slider>();
	}

	private void Update()
	{
		slider.value = Mathf.Lerp(slider.value, toValue, Time.deltaTime * 10);
	}
}
