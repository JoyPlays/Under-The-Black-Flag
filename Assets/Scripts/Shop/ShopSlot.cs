using UnityEngine;
using System.Collections;

public class ShopSlot : MonoBehaviour
{
	public Renderer selectRenderer;
	public Color selectColor;
	public GameObject cannonSlot;

	internal int index;

	private bool _freeSlot = true;
	internal bool freeSlot {
		get { return _freeSlot; }
		set {
			_freeSlot = value;
			if (cannonSlot)
			{
				cannonSlot.SetActive(!_freeSlot);
			}
		}
	}

	private Color startColor;

	// Use this for initialization
	protected  virtual void Start()
	{
		startColor = selectRenderer.material.color;
		freeSlot = _freeSlot;
	}

	protected virtual void OnMouseEnter()
	{
		if (!freeSlot) return;

		selectRenderer.material.color = selectColor;
	}

	protected virtual void OnMouseExit()
	{
		selectRenderer.material.color = startColor;
	}
	protected void Unselect()
	{
		selectRenderer.material.color = startColor;
	}

	protected virtual void OnMouseDown()
	{
		if (SlotClick())
		{
			freeSlot = false;
		}

		selectRenderer.material.color = startColor;
	}

	public void OnSellClick()
	{
		if (_freeSlot) return;

		freeSlot = true;
		Debug.Log("Sell clcicked");
	}

	public virtual bool SlotClick()
	{
		return freeSlot;
	}
}
