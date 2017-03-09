using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
	public static Game Instance;
	public List<City> cities;

	void Awake()
	{
		Instance = this;
		DontDestroyOnLoad(gameObject);
	}
}
