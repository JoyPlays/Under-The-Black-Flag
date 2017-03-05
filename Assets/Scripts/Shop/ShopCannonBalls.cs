using UnityEngine;
using UnityEngine.UI;

public class ShopCannonBalls : MonoBehaviour
{

	public Text textInShip;
	public Text textBuy;

	public int maxAmmo = 500;
	public int currentAmmo = 60;
	internal int selectAmmo = 0;

	private Vector2 startDrag;

	void Start()
	{
		textBuy.gameObject.SetActive(false);
		textInShip.text = currentAmmo.ToString();
	}

	public void OnMouseDown()
	{
		if (currentAmmo >= maxAmmo) return;

		textBuy.gameObject.SetActive(true);
		startDrag = Input.mousePosition;
		selectAmmo = 0;
	}

	public void OnMouseUp()
	{
		currentAmmo += selectAmmo;
		textInShip.text = currentAmmo.ToString();

		selectAmmo = 0;
		textBuy.gameObject.SetActive(false);

	}

	public void OnMouseDrag()
	{
		if (currentAmmo >= maxAmmo) return;

		float delta = startDrag.y - Input.mousePosition.y;

		if (delta >= 0)
		{
			selectAmmo = 0;
		}
		else
		{
			delta = -delta;
			float pd = delta/100f;
			if (pd > 1) pd = 1;

			selectAmmo = Mathf.CeilToInt(pd*(maxAmmo - currentAmmo));
		}

		textBuy.text = selectAmmo.ToString();
	}
}
