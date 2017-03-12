﻿using UnityEngine;
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
		Bullet bullet = null;

//		GameObject obj = Instantiate(bulletPrefab, canon.transform.position, canon.transform.rotation);
//		obj.transform.SetParent(transform);
//		Bullet bullet = obj.GetComponent<Bullet>();
//		bullet.Shot(canon);
		//bullets.Add(bullet);
	}

}
