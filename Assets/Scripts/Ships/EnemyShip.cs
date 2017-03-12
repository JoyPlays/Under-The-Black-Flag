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

		if (PlayerShip.Distance(transform.position) < 15)
		{
			float a = Helper.ClampAngle(transform.eulerAngles.y);
			if (a > 75 && a < 115 || a > 235 && a < 315)
			{
				//Debug.Log("a:" + a + " angle:" + PlayerShip.Angle(transform.position,-a));
				weapons.Shot(PlayerShip.Angle(transform.position, -a));
			}
		}


		if (target && agent.remainingDistance < 0.5f)
		{
			StartCoroutine(WaitInPort());
		}

		base.Update();

	}

	IEnumerator WaitInPort()
	{
		inPort = true;

		City city = target.GetComponent<City>();
		if (city)
		{
			city.TradingWithShip(this);
		}

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
