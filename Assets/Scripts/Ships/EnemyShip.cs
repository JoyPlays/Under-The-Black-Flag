
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : NavShip
{

	public List<Transform> destinations;

	int currentTarget;

	public bool inPort;
	
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
			//Debug.Log("dist:" +  PlayerShip.Distance(transform.position) + " angle:" + PlayerShip.Angle(transform.position));
			weapons.Shot(PlayerShip.Angle(transform.position,-90));
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

		yield return new WaitForSeconds(10);

		currentTarget++;
		if (currentTarget >= destinations.Count)
		{
			currentTarget = 0;
		}

		if (destinations.Count < currentTarget)
		{
			target = destinations[currentTarget];
		}

		inPort = false;
	}

}
