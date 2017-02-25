using UnityEngine;
using System.Collections;

public class DamageManager : MonoBehaviour
{
	[Range(0,1)]
	public float damage;
	public GameObject[] damageLevels;

	// Update is called once per frame
	void Update()
	{
		float t = 1f / damageLevels.Length;

		for (int i = 0; i < damageLevels.Length; i++)
		{
			
			damageLevels[i].SetActive( (i * t) < damage);
		}
	}
}
