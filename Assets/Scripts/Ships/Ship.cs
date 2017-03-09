using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{
	[Header("Params")]
	public float damage;
	public DamageManager damageManager;

	internal bool isDed;

	public void SetHit(float hitpoint)
	{
		if (isDed) return;

		damage += hitpoint;
		if (damageManager) damageManager.damage = damage;
		if (damage >= 1) Sunk();
	}

	public virtual void Sunk()
	{
		isDed = true;
	}

}
