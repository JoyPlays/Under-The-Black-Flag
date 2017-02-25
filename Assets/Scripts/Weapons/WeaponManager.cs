using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class WeaponManager : MonoBehaviour
{
	public Transform bulletsTransform;
	public GameObject bulletPrefab;

	private List<Bullet> bullets = new List<Bullet>();
	private List<Canon> canons;

	void Start()
	{
		canons = GetComponentsInChildren<Canon>().ToList();
	}

	public void Shot(Ship ignoreShip)
	{
		foreach (Canon canon in canons)
		{
			if (!canon.isReady) continue;

			canon.Shot();
			Bullet bullet = null;
			foreach (Bullet b in bullets)
			{
				if (b.isReady)
				{
					bullet = b;
					break;
				}
			}

			if (!bullet)
			{
				GameObject obj = Instantiate(bulletPrefab, canon.transform.position, canon.transform.rotation);
				obj.transform.SetParent(bulletsTransform);
				bullet = obj.GetComponent<Bullet>();
				bullets.Add(bullet);
			}

			bullet.Shot(canon,ignoreShip);
		}		
	}
}
