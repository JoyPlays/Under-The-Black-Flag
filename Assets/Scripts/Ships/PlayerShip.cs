using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : NavShip
{
	[Header("Ship stats")]
	[Range(0, 1000)]
	public float overallHitpoints = 1000;

	[Header("Player")]
	[Range(0, 20)]
	public float maxSpeed = 10;
	[Range(0, 1)]
	public float throtle = 0.1f;
	[Range(0, 10)]
	public float angularThrotle = 1f;
	[Range(0, 50)]
	public float angularSpeed;

	[Header("Test values")]
	[Range(0, 10)]
	public float speed;
	public float targetAngle = 0;

	internal City dockPort;

	private float tAngle;

	void Awake()
	{
		targetAngle = 90;
		Player.hullHitpoints = overallHitpoints;
	}

	protected override void Update()
	{
		base.Update();

		//Keyboard speed
		speed += Input.GetKey(KeyCode.W) ? throtle : Input.GetKey(KeyCode.S) ? -throtle : 0;
		speed = Mathf.Clamp(speed, 0, maxSpeed);

		Player.shipSpeed = speed;

		//Keyboard rotate
		targetAngle += Input.GetKey(KeyCode.D) ? angularThrotle : Input.GetKey(KeyCode.A) ? -angularThrotle : 0;
		float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, angularSpeed * Time.deltaTime) * Mathf.Deg2Rad;

		if (agent && agent.enabled)
		{
			//Set move on nav mesh
			agent.velocity = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * speed;
		}

		if (weapons && Input.GetMouseButtonDown(0))
			//Input.GetKey(KeyCode.Space))
		{

			RaycastHit hit = new RaycastHit();
			if (
					!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition).origin,
													 Camera.main.ScreenPointToRay(Input.mousePosition).direction, out hit, Mathf.Infinity, LayerMask.GetMask("Water")))
			{
				return;
			}
			float a = Helper.AngleInDeg(transform.position, hit.point, -90f);

			Debug.Log("pos: " + transform.position + hit.collider.name + " pos: " + hit.point + " angle:" + a);
			weapons.Shot(a);
		}

		// Set static Player hitpoints
		Player.hullHitpoints = overallHitpoints;
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<City>())
		{
			dockPort = other.GetComponent<City>();
			Debug.Log("Enter in city:" + dockPort.caption);
		}
	}

	public void OnTriggerExit(Collider other)
	{
		if (dockPort && other.GetComponent<City>())
		{
			Debug.Log("Exit from city:" + dockPort.caption);
			dockPort = null;
		}

	}
}
