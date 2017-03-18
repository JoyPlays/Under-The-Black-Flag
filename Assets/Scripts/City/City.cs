using UnityEngine;
using System.Collections;

public class City : MonoBehaviour
{

	public string caption;

	public TextMesh text;

	public float safeDistance = 100;

	public string CityName()
	{
		return caption;
	}

	void Start()
	{
		//if (text)
		{
			text.text = caption;
		}

	}

	void Update()
	{
	}

	public float Distance(Vector3 position)
	{
		return Vector3.Distance(transform.position.zerroY(), position.zerroY());
	}

	public void TradingWithShip(Ship ship)
	{

	}
}
