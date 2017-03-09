
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : NavShip
{
	public List<Transform> destinations;

	int currentTarget;

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
		if (agent.remainingDistance < 0.5f && destinations.Count > 0)
		{
			currentTarget++;
			if (currentTarget > destinations.Count)
			{
				currentTarget = 0;
			}

			target = destinations[currentTarget];
		}

		base.Update();


	}
}
