using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ShopButton : MonoBehaviour
{

	public UnityEvent MouseClick;

	public void OnMouseDown()
	{
		MouseClick.Invoke();
	}
}
