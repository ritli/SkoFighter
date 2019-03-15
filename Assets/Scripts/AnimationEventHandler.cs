using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnAnimationEventTrigger(int index);

public class AnimationEventHandler : MonoBehaviour
{
	public OnAnimationEventTrigger onAnimationEventTrigger;

	public void TriggerAnimationEvent(int index)
	{
		onAnimationEventTrigger?.Invoke(index);
	}
}
