using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ShopCannonSlot : ShopSlot
{
	public CannonBort bort;
	public CannonDeck deck;


	protected override void OnMouseEnter()
	{
		if (!CannonShopManager.SelectedCannon) return;
		base.OnMouseEnter();
	}

	public override bool SlotClick()
	{
		if (!base.SlotClick()) return false;

		return CannonShopManager.Instance.BuyCannon(this);
	}

}
