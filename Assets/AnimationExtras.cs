using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationExtras : MonoBehaviour
{

	public Animator shipShop;


	public void SetTriggerShip(string trigger)
	{
		shipShop.SetTrigger(trigger);
	}
}
