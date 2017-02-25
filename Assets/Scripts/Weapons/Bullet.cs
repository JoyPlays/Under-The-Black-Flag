using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

	private Rigidbody body;
	private Vector3 start;

	void Awake()
	{
		start = transform.position;
	}
	void Start()
	{
		body = GetComponent<Rigidbody>();
	}

	public void OnEnable()
	{
		transform.position = start;
	}

	public void OnDisable()
	{
		transform.position = start;
	}

	public void Shot(Vector3 direction, float force)
	{
		body.AddForce(direction * force);
		Invoke("EndShot",1);
	}

	private void EndShot()
	{
		gameObject.SetActive(false);
	}

}
