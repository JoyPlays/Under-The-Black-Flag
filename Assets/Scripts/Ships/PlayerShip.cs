using UnityEngine;
using UnityEngine.AI;

public class PlayerShip : NavShip
{
	[Header("Player")]
	public float targetAngle = 0;

	public float maxSpeed = 10;
	public float throtle = 0.1f;
	public float angularThrotle = 1f;

	[Range(0, 50)]
	public float angularSpeed;

	[Range(0, 10)]
	public float speed;

	public WeaponManager weapons;

	private float tAngle;

	void Awake()
	{
		targetAngle = 90;
	}

	protected override void Update()
	{
		base.Update();

		//Keyboard speed
		speed += Input.GetKey(KeyCode.W) ? throtle : Input.GetKey(KeyCode.S) ? -throtle : 0;
		speed = Mathf.Clamp(speed, 0, maxSpeed);

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
	}

	public void OnCollisionStay(Collision collision)
	{
	}
}
