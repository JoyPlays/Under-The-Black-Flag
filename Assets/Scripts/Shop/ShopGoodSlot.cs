using UnityEngine;
using System.Collections;

public class ShopGoodSlot : ShopSlot
{

	public string goodName;
	public int goodAmount;

	protected override void Start()
	{
		selectRenderer = GetComponent<Renderer>();
		base.Start();
		
	}

	public override bool SlotClick()
	{
		if (!base.SlotClick()) return false;
		if (!ShopGoodResource.SelectedResorce || string.IsNullOrEmpty(ShopGoodResource.SelectedResorce.goodName)) return false;

		Debug.Log("On good slot click: " + ShopGoodResource.SelectedResorce.goodName);

		goodName = ShopGoodResource.SelectedResorce.goodName;
		goodAmount++;

		ShopGoodResource.SelectedResorce.selected = false;
		ShopGoodResource.SelectedResorce = null;

		return true;
	}
}
