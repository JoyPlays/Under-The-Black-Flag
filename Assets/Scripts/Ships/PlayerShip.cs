using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

	[Header("Cannons")]
	public List<Canon> rightCannons;
	public List<Canon> leftCannons;

	[Header("Test values")]
	[Range(0, 10)]
	public float speed;
	public float targetAngle = 0;



	public WeaponManager weapons;

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

		if (weapons && Input.GetKey(KeyCode.Space))
		{
			//weapons.Shot(this);
		}

        // Set static Player hitpoints
        Player.hullHitpoints = overallHitpoints;
    }

}
