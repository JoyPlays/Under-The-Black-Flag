using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class NavShip : Ship
{

	public Transform target;

	[Header("Wave")]
	[Range(0, 3)]
	public float amplitude = 1;
	[Range(0, 1)]
	public float frequency = 1;

	[Header("Sunk")]
	public float sunkSpeed = 2f;

	[Header("Params")]
	public float damage;
	public DamageManager damageManager;

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

	public void SetHit(float hitpoint)
	{
		if (!agent || !agent.enabled) return;

		damage += hitpoint;
		if (damageManager) damageManager.damage = damage;
		if (damage >= 1) Sunk();
	}

	public void Sunk()
	{
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
