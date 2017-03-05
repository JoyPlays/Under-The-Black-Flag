using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class NavShip : MonoBehaviour
{

	public Transform target;

	[Header("Wave")]
	[Range(0, 3)]
	public float amplitude = 1;
	[Range(0, 1)]
	public float frequency = 1;


	private NavMeshAgent agent;


	// Use this for initialization
	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	// Update is called once per frame
	void Update()
	{

		agent.baseOffset += amplitude*(Mathf.Sin(2*Mathf.PI*frequency*Time.time) -Mathf.Sin(2*Mathf.PI*frequency*(Time.time - Time.deltaTime)));
		agent.SetDestination(target.position);

	}

}
