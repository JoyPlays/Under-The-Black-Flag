﻿using UnityEngine;
using System.Collections;

public class Canon : MonoBehaviour
{
	public float reloadTime = 3f;
	public float force = 100;

	[Header("Random values (0-100%)")]

	[Tooltip("Reaload time increase random")]
	[Range(0,100)]
	public float reloadTimeIncrease = 10;

	[Tooltip("Force increase random")]
	[Range(0, 100)]
	public float forceIncrease = 10;

	internal bool isReady = true;

	internal Ship ship;

	void Start()
	{
		ship = GetComponentInParent<Ship>();
	}

	internal Vector3 shotForce
	{
		get
		{
			return transform.forward * (force + (1 + Random.value*(forceIncrease/100f)));
		}
	}

	public void Shot()
	{
		if (!isReady) return;

		isReady = false;
		StartCoroutine(Shooting());
	}

	IEnumerator Shooting()
	{
		BulletManager.Shot(this);

		yield return new WaitForSeconds(reloadTime * (1 + Random.value * (reloadTimeIncrease / 100f)));

		isReady = true;
	}

	public void OnDrawGizmos()
	{
		Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);

		Color oldColor = Gizmos.color;
		Gizmos.color = Color.blue;

		Gizmos.DrawWireCube(Vector3.zero + Vector3.forward * 0.5f, new Vector3(0.1f, 0.1f, 1f));

		Gizmos.matrix = Matrix4x4.identity;
		Gizmos.color = oldColor;

	}

	public void OnDrawGizmosSelected()
	{
		Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);

		Color oldColor = Gizmos.color;
		Gizmos.color = Color.green;

		Gizmos.DrawCube(Vector3.zero + Vector3.forward * 0.5f, new Vector3(0.1f, 0.1f, 1f));

		Gizmos.matrix = Matrix4x4.identity;
		Gizmos.color = oldColor;

	}
}
