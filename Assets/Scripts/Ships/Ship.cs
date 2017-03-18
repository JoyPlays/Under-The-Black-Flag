using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{
	public string shipName;

	[Header("Params")]
	public float damage;
	public WeaponManager weapons;

	internal bool isDed;

	public void SetHit(float hitpoint)
	{
		if (isDed) return;

		//damage += hitpoint;
		if (damage >= 1) Sunk();

	}

	public virtual void Sunk()
	{
		isDed = true;
	}

}
