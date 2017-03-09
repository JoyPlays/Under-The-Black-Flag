using UnityEngine;
using System.Collections;

public class ShopGoodResource : MonoBehaviour
{

	public static string SelectedResorce;

	public GameObject selectedObject;

	public EntityEconomy economy;
	public string goodName;

	private bool _selected;

	public bool selected {
		get { return _selected; }
		set {
			_selected = value;
			selectedObject.SetActive(_selected);
		}
	}

	// Use this for initialization
	void Start()
	{
		selectedObject.SetActive(false);
	}

	public void OnMouseDown()
	{
		SelectedResorce = goodName;
	}
}
