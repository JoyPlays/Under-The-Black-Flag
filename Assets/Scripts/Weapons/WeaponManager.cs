using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class WeaponManager : MonoBehaviour
{
	internal List<Canon> canons;

	void Start()
	{
		canons = GetComponentsInChildren<Canon>().ToList();
		int idx = 0;
		foreach (Canon canon in canons)
		{
			canon.index = idx++;
		}
	}

	public void Shot(float angle)
	{

		CannonBort bort = angle < 180 ? CannonBort.Right : CannonBort.Left;

		foreach (Canon canon in canons)
		{
			if (canon.bort != bort) continue;
			canon.Shot();
		}		
	}
}
