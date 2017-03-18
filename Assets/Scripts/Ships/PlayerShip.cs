using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : NavShip
{
	public static PlayerShip Instance;

	[Header("Ship stats")]
	[Range(0, 1000)]
	public float overallHitpoints = 1000;

	[Header("Player")]
	[Range(0, 20)]
	public float maxSpeed = 10;
	[Range(0, 1)]
	public float throtle = 0.1f;
	[Range(0, 1)]
	public float throtlePort = 0.09f;
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

	public static float Distance(Vector3 position)
	{
		if (!Instance) return Mathf.Infinity;

		Vector3 from = new Vector3(Instance.transform.position.x, 0, Instance.transform.position.z);
		Vector3 to = new Vector3(position.x, 0, position.z);
		return Vector3.Distance(from, to);

	}

	public static float Angle(Vector3 position, float delta = 0)
	{
		if (!Instance) return 0;

		Vector3 from = new Vector3(Instance.transform.position.x, 0, Instance.transform.position.z);
		Vector3 to = new Vector3(position.x, 0, position.z);

		return Helper.AngleInDeg(to, from, delta);

	}

	void Awake()
	{
		Instance = this;

		//targetAngle = 90;
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

		transform.eulerAngles = new Vector3(0, angle * Mathf.Rad2Deg, 0);

		if (agent && agent.enabled)
		{
			//Set move on nav mesh
			agent.velocity = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * speed;
		}

		if (weapons && Input.GetMouseButtonDown(0))
		//Input.GetKey(KeyCode.Space))
		{

			RaycastHit hit = new RaycastHit();
			if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Camera.main.ScreenPointToRay(Input.mousePosition).direction, out hit, Mathf.Infinity, LayerMask.GetMask("Water")))
			{
				return;
			}
			angle = Helper.AngleInDeg(transform.position, hit.point, -angle * Mathf.Rad2Deg);
			weapons.Shot(angle);
		}

		// Set static Player hitpoints
		Player.hullHitpoints = overallHitpoints * (1 - damage);
	}

#region PORT
	public void OnTriggerEnter(Collider other)
	{
		if (!other.GetComponent<City>()) return;

		dockPort = other.GetComponent<City>();
		Player.enteredCity = dockPort.caption;

		CityManager.CurrentCity = dockPort;

		Debug.Log("Enter in city:" + dockPort.caption);
	}

	public void OnTriggerStay(Collider other)
	{
		if (!other.GetComponent<City>()) return;

		if (speed > 0) speed -= throtlePort;

	}

	public void OnTriggerExit(Collider other)
	{
		if (dockPort && other.GetComponent<City>())
		{
			Debug.Log("Exit from city:" + dockPort.caption);
			dockPort = null;
			CityManager.CurrentCity = null;
			Player.enteredCity = "";
		}

	}
#endregion
}
