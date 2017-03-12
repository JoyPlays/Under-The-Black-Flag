using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class WeaponManager : MonoBehaviour
{
	private List<Canon> canons;

	void Start()
	{
		canons = GetComponentsInChildren<Canon>().ToList();
	}

	public void Shot()
	{
		foreach (Canon canon in canons)
		{
			canon.Shot();
		}		
	}
}
