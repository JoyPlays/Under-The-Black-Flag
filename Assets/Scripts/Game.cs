using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Game : MonoBehaviour
{
	public static Game Instance;

	internal List<City> cities;

	void Awake()
	{
		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		cities = GetComponentsInChildren<City>().ToList();
	}
}
