using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	public float hitpoint = 0.1f;
	public GameObject effect;
	public GameObject visuals;

	internal bool isReady = true;

	private Rigidbody body;
	private bool isCollision;
	private float bulletTime = 2f;

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

	public void Shot(Canon canon)
	{
		if (!isReady) return;

		gameObject.SetActive(true);

		if (canon.ship)
		{
			ignoredColliders = canon.ship.GetComponentsInChildren<Collider>();
			IgnoreColliders(true);
		}

		transform.position = canon.transform.position;
		transform.rotation = canon.transform.rotation;
		visuals.SetActive(true);

		body.AddForce(canon.shotForce);

		StartCoroutine(WaitShot());
	}

	IEnumerator WaitShot()
	{
		isReady = false;
		isCollision = false;

		float t = 0;
		while (t < bulletTime)
		{
			t += Time.deltaTime;
			yield return null;
			if (isCollision)
			{
				break;
			}
		}

		if (isCollision)
		{
			yield return new WaitForSeconds(2);
		}

		IgnoreColliders(false);

		body.velocity = Vector3.zero;
		body.angularVelocity = Vector3.zero;

		transform.localPosition = Vector3.zero;
		transform.localEulerAngles = Vector3.zero;

		isReady = true;

		if (effect) effect.SetActive(false);
		gameObject.SetActive(false);
	}

	public void OnCollisionEnter(Collision collision)
	{
		
		if (isCollision) return;

		Ship ship = collision.gameObject.GetComponentInParent<Ship>();
		if (!ship)
		{
			return;
		}

		//Debug.Log("Damage ship");
		ship.SetHit(hitpoint);

		body.velocity = Vector3.zero;
		body.angularVelocity = Vector3.zero;

		visuals.SetActive(false);

		if (effect) effect.SetActive(true);

		isCollision = true;
		
	}
}
