using UnityEngine;
using System.Collections;

public class ShopGoodSlot : ShopSlot
{
	public override bool SlotClick()
	{
		if (!base.SlotClick()) return false;

		Debug.Log("On good slot click");

		return true;
	}
}
