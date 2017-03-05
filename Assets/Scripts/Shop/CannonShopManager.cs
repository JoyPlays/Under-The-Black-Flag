using UnityEngine;
using System.Collections;

public class CannonShopManager : MonoBehaviour
{

	public static CannonShopManager Instance;
	private static ShopCannonCannon selectedCannon;

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

			Instance.Gui.SetActive(selectedCannon);
		}
	}

	public GameObject Gui;

	public void Awake()
	{
		Instance = this;
	}
	

	// Use this for initialization
	void Start()
	{
		Gui.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		if (selectedCannon && Input.GetMouseButtonUp(1))
		{
			SelectedCannon = null;
		}
	}
}
