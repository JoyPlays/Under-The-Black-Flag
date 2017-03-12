using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class NavShip : Ship
{
	[Header("Navigate")]
	public Transform target;

	[Header("Wave")]
	[Range(0, 3)]
	public float amplitude = 1;
	[Range(0, 1)]
	public float frequency = 1;

	[Header("Sunk")]
	public float sunkSpeed = 2f;


	protected NavMeshAgent agent;


	// Use this for initialization
	protected virtual void Start()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	// Update is called once per frame
	protected virtual void Update()
	{
		if (agent && agent.enabled)
		{
			agent.baseOffset += amplitude * (Mathf.Sin(2 * Mathf.PI * frequency * Time.time) - Mathf.Sin(2 * Mathf.PI * frequency * (Time.time - Time.deltaTime)));
			if (target)
			{
				agent.SetDestination(target.position);
			}
		}
	}

	

	public override void Sunk()
	{
		base.Sunk();

		if (!agent || !agent.enabled) return;
		StartCoroutine(SunkShip());
	}

	IEnumerator SunkShip()
	{
		while (agent.baseOffset > -50)
		{
			agent.baseOffset -= sunkSpeed * Time.deltaTime;
			yield return null;
		}

		agent.enabled = false;
		Destroy(gameObject,0.5f);
	}

}
