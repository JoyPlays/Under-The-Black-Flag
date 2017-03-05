using UnityEngine;
using System.Collections;

public class ShopCannonSlot : MonoBehaviour
{

	public Renderer selectRenderer;
	public Color selectColor;
	public GameObject cannonSlot;

	private bool _freeSlot = true;
	internal bool freeSlot {
		get { return _freeSlot; }
		set {
			_freeSlot = value;
			cannonSlot.SetActive(!_freeSlot);
		}
	}

	private Color startColor;

	// Use this for initialization
	void Start()
	{
		startColor = selectRenderer.material.color;
		freeSlot = _freeSlot;
	}

	public void OnMouseEnter()
	{
		if (!CannonShopManager.SelectedCannon) return;
		if (!freeSlot) return;

		selectRenderer.material.color = selectColor;
	}

	public void OnMouseExit()
	{
		selectRenderer.material.color = startColor;
	}

	public void OnMouseDown()
	{
		if (!CannonShopManager.SelectedCannon) return;
		if (!freeSlot) return;

		freeSlot = false;

		CannonShopManager.SelectedCannon.gameObject.SetActive(false);
		CannonShopManager.SelectedCannon = null;

		selectRenderer.material.color = startColor;
	}
}
