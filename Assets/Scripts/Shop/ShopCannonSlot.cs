using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ShopCannonSlot : ShopSlot
{


	protected override void OnMouseEnter()
	{
		if (!CannonShopManager.SelectedCannon) return;
		base.OnMouseEnter();
	}

	public override bool SlotClick()
	{
		if (!base.SlotClick()) return false;
		if (!CannonShopManager.SelectedCannon) return false;

		CannonShopManager.SelectedCannon.gameObject.SetActive(false);
		CannonShopManager.SelectedCannon = null;

		return true;
	}

}
