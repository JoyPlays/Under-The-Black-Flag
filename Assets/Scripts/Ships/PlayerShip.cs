using UnityEngine;

public class PlayerShip : Ship
{
	[Header("Player")]
	public float maxSpeed = 10;
	public float throtle = 0.1f;
	public float angularThrotle = 1f;

	public WeaponManager weapons;

	private float tAngle;

	// Update is called once per frame
	protected override void Update()
	{
		//Keyboard speed
		speed += Input.GetKey(KeyCode.W) ? throtle : Input.GetKey(KeyCode.S) ? -throtle : 0;
		speed = Mathf.Clamp(speed, 0, maxSpeed);

		//Keyboard rotate
		targetAngle += Input.GetKey(KeyCode.D) ? angularThrotle : Input.GetKey(KeyCode.A) ? -angularThrotle : 0;

		//Do default move
		base.Update();

		if (weapons && Input.GetKey(KeyCode.Space))
		{
			weapons.Shot();
		}

	}

	public void OnCollisionStay(Collision collision)
	{
		//Debug.Log("Collision:" + collision.gameObject.tag);
		if (collision.gameObject.CompareTag("Ground")) speed = 0;
	}
}
