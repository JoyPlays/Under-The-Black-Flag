using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CityManager : MonoBehaviour
{
	public static CityManager Instance;
	public static City CurrentCity;

	private List<City> cityList;


	public static City ClosestCity(Vector3 position, out float distance)
	{
		
		City closest = null;
		distance = Mathf.Infinity;

		foreach (var obj in Instance.cityList)
		{
			float delta = Vector3.Distance(position, obj.transform.position);

			if (delta < distance)
			{
				closest = obj;
				distance = delta;
			}
		}

		return closest;
	}

	void Awake()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start()
	{
		cityList = GetComponentsInChildren<City>().ToList();
	}




}
