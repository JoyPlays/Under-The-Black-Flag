using UnityEngine;
using System.Collections.Generic;

public class BulletManager : MonoBehaviour
{
	public static BulletManager Instance;

	public GameObject bulletPrefab;

	public Canon testCannon;

	private List<Bullet> bullets = new List<Bullet>();

	void Awake()
	{
		Instance = this;
	}
	

	void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			ShotBullet(testCannon);
		}
	}

	public static void Shot(Canon canon)
	{
		if (!Instance) return;
		Instance.ShotBullet(canon);
	}

	public void ShotBullet(Canon canon)
	{
		if (!canon)
		{
			return;
		}

		Bullet bullet = null;
		foreach (Bullet bullet1 in bullets)
		{
			if (!bullet1.isReady) continue;
			bullet = bullet1;
		}
		if (!bullet)
		{
			GameObject obj = Instantiate(bulletPrefab, canon.transform.position, canon.transform.rotation);
			obj.transform.SetParent(transform);
			bullet = obj.GetComponent<Bullet>();
			bullets.Add(bullet);
		}

		bullet.Shot(canon);

	}

}
