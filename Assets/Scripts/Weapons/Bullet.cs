using UnityEngine;
using System.Collections;
using NUnit.Framework;

public class Bullet : MonoBehaviour
{
	public GameObject effect;

	internal bool isReady = true;

	private Rigidbody body;
	private bool isCollision;
	private float bulletTime = 1.5f;

	private Collider[] ignoredColliders;
	private Collider selfCollider;

	void Awake()
	{
		body = GetComponent<Rigidbody>();
		selfCollider = GetComponentInChildren<Collider>();
	}

	void IgnoreColliders(bool ignore)
	{
		if (!selfCollider) return;
		if (ignoredColliders == null) return;
		foreach (Collider collider1 in ignoredColliders)
		{
			Physics.IgnoreCollision(selfCollider, collider1, ignore);
		}
	}

	public void Shot(Canon canon, Ship ignoreShip)
	{
		gameObject.SetActive(true);

		if (ignoreShip)
		{
			ignoredColliders = ignoreShip.GetComponentsInChildren<Collider>();
			IgnoreColliders(true);
		}

		transform.position = canon.transform.position;
		transform.rotation = canon.transform.rotation;

		body.AddForce(canon.transform.forward * canon.force * 100);

		StartCoroutine(WaitShot());
	}

	IEnumerator WaitShot()
	{
		isReady = false;
		isCollision = false;

		float t = bulletTime;
		while (t > 0)
		{
			t -= Time.deltaTime;

			if (isCollision)
			{
				yield return new WaitForSeconds(4);
			}

		yield return null;
		}

		IgnoreColliders(false);

		body.velocity = Vector3.zero;
		body.angularVelocity = Vector3.zero;

		transform.localPosition = Vector3.zero;
		transform.localEulerAngles = Vector3.zero;

		if (effect) effect.SetActive(false);
		gameObject.SetActive(false);
		isReady = true;
	}

	public void OnCollisionEnter(Collision collision)
	{
		/*
		if (isCollision) return;

		Ship ship = collision.gameObject.GetComponentInParent<Ship>();
		if (ship)
		{
			ship.SetHit(0.1f);
		}

		body.velocity = Vector3.zero;
		body.angularVelocity = Vector3.zero;
		if (effect) effect.SetActive(true);

		isCollision = true;
		*/
	}
}
