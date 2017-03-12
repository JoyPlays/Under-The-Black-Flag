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


	public float playerAngle;
	public float deltaAngle;

	public float a1;
	public float a2;
	public float a3;

	public float angle;
	[Range(0, 50)]
	public float angularSpeed;
	int currentTarget;

	internal bool inPort;

	protected override void Start()
	{
		base.Start();

		if (destinations.Count > 0)
		{
			//target = destinations[0];
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
			angle = Helper.ClampAngle(transform.eulerAngles.y);
			playerAngle = PlayerShip.Angle(transform.position,90);
			deltaAngle = Helper.ClampAngle(playerAngle - angle);
			if (deltaAngle >= 0 && deltaAngle <= 25 || deltaAngle >= 335)
			{
				//Debug.Log("a:" + a + " angle:" + PlayerShip.Angle(transform.position,-a));
				weapons.Shot(PlayerShip.Angle(transform.position, -angle));
			}
			//else
			{
				a1 = Mathf.Abs(Helper.CalcShortestRot(angle, playerAngle));
				a2 = Mathf.Abs(Helper.CalcShortestRot(angle, playerAngle - 180f));

				a3 = a1 < a2 ? playerAngle : playerAngle - 180;

				angle = Mathf.MoveTowardsAngle(angle, a3, angularSpeed*Time.deltaTime);


				transform.eulerAngles = new Vector3(0, angle, 0);
			}
			if (playerDist > shotDistance*0.5f)
			{
				target = PlayerShip.Instance.transform;
			}
			else
			{
				target = null;
				agent.velocity = Vector3.zero;
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
