using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CannonShopManager : MonoBehaviour
{
	public static CannonShopManager Instance;
	private static ShopCannonCannon selectedCannon;

	internal CannonDeck CurrentDeck = CannonDeck.Middle;

	public static ShopCannonCannon SelectedCannon
	{
		get { return selectedCannon; }
		set
		{
			if (selectedCannon)
			{
				selectedCannon.selected = false;
			}
			selectedCannon = value;

			if (selectedCannon)
			{
				selectedCannon.selected = true;
			}

		}
	}

	public CannonData cannonData;

	public Slider deckSlider;



	private List<Canon> shipCanons;
	private ShopCannonSlot[] slots;

	public void Awake()
	{
		Instance = this;
	}
	
	// Use this for initialization
	void Start()
	{
		slots = GetComponentsInChildren<ShopCannonSlot>();

		shipCanons = PlayerShip.Instance.weapons.canons;
		DeckClick(1);
	}

	void RecalcSlots()
	{
		foreach (ShopCannonSlot slot in slots)
		{
			slot.gameObject.SetActive(slot.deck == CurrentDeck);
		}

		List<Canon> current = new List<Canon>();
		CannonBort bort = CannonBort.Left;
		foreach (Canon canon in shipCanons)
		{
			if (canon.deck == CurrentDeck && canon.bort == bort )
			{
				current.Add(canon);
			}
		}
		int idx = 0;
		foreach (ShopCannonSlot slot in slots)
		{
			if (slot.bort != bort) continue;
			if (idx >= current.Count) break;

			slot.freeSlot = !current[idx].isActive;
			slot.index = current[idx].index;

			idx++;
		}

		current.Clear();
		bort = CannonBort.Right;
		foreach (Canon canon in shipCanons)
		{
			if (canon.deck == CurrentDeck && canon.bort == bort)
			{
				current.Add(canon);
			}
		}
		idx = 0;
		foreach (ShopCannonSlot slot in slots)
		{
			if (slot.bort != bort) continue;
			if (idx >= current.Count) break;

			slot.freeSlot = !current[idx].isActive;
			slot.index = current[idx].index;

			idx++;
		}

	}

	// Update is called once per frame
	void Update()
	{
		if (selectedCannon && Input.GetMouseButtonUp(1))
		{
			SelectedCannon = null;
		}
	}

	public bool BuyCannon(ShopCannonSlot slot)
	{
		if (!SelectedCannon) return false;

		CannonParams param = cannonData.cannons[SelectedCannon.paramIndex];
		if (Player.money < param.cost)
		{
			GameUI.ShowMessage(UIMessageType.Error, "Not money!");
			return false;
		}

		shipCanons[slot.index].isActive = true;

		SelectedCannon.gameObject.SetActive(false);
		SelectedCannon = null;

		return true;

	}

	#region GUI
	public void DeckClick(int deck)
	{
		deckSlider.value = deck;
	}

	public void DeckSelect(float deck)
	{
		CurrentDeck = (CannonDeck)deck;
		RecalcSlots();
		Debug.Log("Selected deck:" + CurrentDeck);
	}
#endregion
}
