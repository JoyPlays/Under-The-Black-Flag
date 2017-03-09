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
		if (!ShopGoodResource.SelectedResorce) return false;

		Debug.Log("On good slot click: " + ShopGoodResource.SelectedResorce.goodName);

		goodName = ShopGoodResource.SelectedResorce.goodName.ToString();
		ShopGoodUI.Instance.SetResource(goodName, this);

		/*
		goodName = ShopGoodResource.SelectedResorce.goodName.ToString();
		goodAmount++;

		ShopGoodResource.SelectedResorce.selected = false;
		ShopGoodResource.SelectedResorce = null;
		*/
		return true;
	}


	public void BuyResource(string name, int amount)
	{
		goodName = name;
		goodAmount = amount;

		ShopGoodResource.SelectedResorce.selected = false;
		ShopGoodResource.SelectedResorce = null;

		Unselect();
	}
}
