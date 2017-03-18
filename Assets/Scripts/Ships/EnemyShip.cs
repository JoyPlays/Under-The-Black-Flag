using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : NavShip
{
	[Header("NPC destinations")]
	public bool randomDestination;
	[Tooltip("secconds for waiting in target")]
	public float waitInPort = 10;
	public List<Transform> destinations;

	[Header("Shoting")]
	public float shotDistance = 20;
	public float shotAngle = 25;
	[Range(0, 50)]
	public float angularSpeed;

	[Header("Debug values")]
	public float deltaAngle;

	int currentTarget;

	internal bool inPort;

	protected override void Start()
	{
		base.Start();

		if (destinations.Count > 0)
		{
			target = destinations[0];
		}
	}

	protected override void Update()
	{

		if (inPort)
		{
			base.Update();
			return;
		}
		float playerDist = PlayerShip.Distance(transform.position);
		if (playerDist < shotDistance)
		{
			float angle = Helper.ClampAngle(transform.eulerAngles.y);
			float playerAngle = PlayerShip.Angle(transform.position,90);
			deltaAngle = Helper.ClampAngle(playerAngle - angle);


			if (deltaAngle >= 360 - shotAngle ||  deltaAngle <= shotAngle || deltaAngle >= 180 - shotAngle && deltaAngle <= 180 + shotAngle )
			{
				//Debug.Log("shot:" + shipName);
				weapons.Shot(PlayerShip.Angle(transform.position, -angle));
			}
			float a1 = Mathf.Abs(Helper.CalcShortestRot(angle, playerAngle));
			float a2 = Mathf.Abs(Helper.CalcShortestRot(angle, playerAngle - 180f));

			float a3 = a1 < a2 ? playerAngle : playerAngle - 180;

			angle = Mathf.MoveTowardsAngle(angle, a3, angularSpeed*Time.deltaTime);


			transform.eulerAngles = new Vector3(0, angle, 0);

			if (playerDist > shotDistance*0.8f)
			{
				target = PlayerShip.Instance.transform;
			}
			else
			{
				target = null;
				agent.velocity = Vector3.zero;
				agent.ResetPath();
			}


		}
		else if (playerDist > shotDistance*2)
		{
			target = destinations[currentTarget];
		}


		if (target && Vector3.Distance(transform.position, target.position) < 0.5f)
		{
			StartCoroutine(WaitInPort());
		}

		base.Update();
	}

	IEnumerator WaitInPort()
	{

		City city = target.GetComponent<City>();
		if (!city)
		{
			yield break;
		}

		Debug.Log("Wait in port");

		inPort = true;
		city.TradingWithShip(this);
		target = null;

		yield return new WaitForSeconds(waitInPort);

		if (randomDestination && destinations.Count > 1)
		{
			int d = Random.Range(0, destinations.Count);
			while (d == currentTarget)
			{
				d = Random.Range(0, destinations.Count);
			}
		}
		else
		{
			currentTarget++;
			if (currentTarget >= destinations.Count)
			{
				currentTarget = 0;
			}
		}


		if (destinations.Count < currentTarget)
		{
			target = destinations[currentTarget];
		}

		inPort = false;
	}

}
