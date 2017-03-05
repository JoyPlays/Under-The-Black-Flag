using UnityEngine;
using System.Collections;

public class ShopCannonCannon : MonoBehaviour
{

	public GameObject selectedObject;

	private bool _selected;

	public bool selected
	{
		get { return _selected; }
		set
		{
			_selected = value;
			selectedObject.SetActive(_selected);
		}
	}

	// Use this for initialization
	void Start()
	{
		selectedObject.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void OnMouseDown()
	{
		CannonShopManager.SelectedCannon = this;
	}
}
