using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopGoodUI : MonoBehaviour
{

	public static ShopGoodUI Instance;

	public Text textCaption;

	public Text textShipAmount;
	public Text textShopAmount;
	public Slider amountSlider;

	public Text textBuyPrice;
	public Text textSellPrice;


	internal int maxAmount;
	internal int shipAmount;
	internal int shopAmount;

	internal int currentAmount;


	public GameObject panelGui;

	public Shop shop;

	ShopGoodSlot selectedSlot;

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		panelGui.SetActive(false);
	}

	public void SetResource(string resource, ShopGoodSlot slot)
	{
		selectedSlot = slot;

		EntityEconomy economy = shop.economy;

		if (economy)
		{
			//textBuyPrice.text = economy.GetResourceBuyPrices(resource).ToString();
			//textSellPrice.text = economy.GetResourceSellPrices(resource).ToString();
			shopAmount = economy.GetResourceAmount(resource);
		}

		maxAmount = 50;
		shipAmount = slot.goodAmount;

		amountSlider.value = (float) shipAmount / maxAmount;

		textCaption.text = resource;

		panelGui.SetActive(true);

	}

	public void ClosePanel()
	{
		panelGui.SetActive(false);
	}

	void Update()
	{
		textShipAmount.text = shipAmount.ToString();
		textShopAmount.text = shopAmount.ToString();

		if (Input.GetMouseButtonDown(1))
		{
			ClosePanel();
		}
	}

	public void SlideChange(float value)
	{
		int curr = Mathf.CeilToInt(maxAmount * value);
		currentAmount += curr - shipAmount;
		shopAmount -= curr - shipAmount;
		shipAmount = curr;
	}

	public void BuyResource()
	{
		selectedSlot.BuyResource(textCaption.text, currentAmount);
		ClosePanel();
	}
}
