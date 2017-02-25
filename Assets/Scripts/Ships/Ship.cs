using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{

	[Range(0, 360)]
	public float targetAngle;

	[Range(0, 10)]
	public float angularSpeed;

	[Range(0, 10)]
	public float speed;

	[Header("Wave")]
	public float amplitude = 1;
	public float frequency = 1;

	[Header("Params")]
	public float damage;
	public DamageManager damageManager;

	protected bool isDed;

	protected virtual void Update()
	{
		float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, angularSpeed * Time.deltaTime);
		transform.eulerAngles = new Vector3(0, angle, 0);
		transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);

		//Wave
		transform.position += amplitude * (Mathf.Sin(2 * Mathf.PI * frequency * Time.time) - Mathf.Sin(2 * Mathf.PI * frequency * (Time.time - Time.deltaTime))) * transform.up;
	}

	public void SetHit(float hitpoint)
	{
		if (isDed) return;

		damage += hitpoint;
		if (damageManager) damageManager.damage = damage;
		if (damage >= 1) Sunk();
	}

	public void Sunk()
	{
		if (isDed) return;
		;
		isDed = true;
		if (damageManager) damageManager.gameObject.SetActive(false);

		Rigidbody body = GetComponent<Rigidbody>();

		body.constraints = RigidbodyConstraints.None;
		body.useGravity = true;

		Destroy(gameObject,2.5f);
	}
}
