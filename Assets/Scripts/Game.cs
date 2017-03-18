using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Game : MonoBehaviour
{
	public static Game Instance;


	void Awake()
	{
		Instance = this;
		DontDestroyOnLoad(gameObject);

		Physics.IgnoreLayerCollision(11, 4, true);

	}

	void Start()
	{
	}
}
