using UnityEngine;
using System.Collections;

public enum CannonBort
{
	Left, Right
}

public enum CannonDeck
{
	Upper, Middle, Down
}

public class Canon : MonoBehaviour
{
	public float reloadTime = 3f;
	public float force = 100;

	public CannonBort bort;
	public CannonDeck deck;

	public AudioClip cannonSound;

	[Header("Random values (0-100%)")]

	[Tooltip("Reaload time increase random")]
	[Range(0,100)]
	public float reloadTimeIncrease = 10;

	[Tooltip("Force increase random")]
	[Range(0, 100)]
	public float forceIncrease = 10;

	public Vector3 forward;

	public bool isActive = true;
	internal bool isReady = true;

	internal Ship ship;
	internal int index;

	void Start()
	{
		ship = GetComponentInParent<Ship>();
	}

	internal Vector3 shotForce
	{
		get
		{
			//Debug.Log(transform.forward);
			forward = transform.forward;
			return forward * force  * 10f * (1 + (Random.value * forceIncrease) / 100f);
		}
	}

	public void Shot()
	{
		if  (!isActive) return;

		if (!isReady) return;
		isReady = false;
		StartCoroutine(Shooting());
	}

	IEnumerator Shooting()
	{
		BulletManager.Shot(this);
		//AudioSource.PlayClipAtPoint(cannonSound, transform.position);
		yield return new WaitForSeconds(reloadTime * (1 + Random.value * (reloadTimeIncrease / 100f)));

		isReady = true;
	}

	public void OnDrawGizmos()
	{
		Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);

		Color oldColor = Gizmos.color;
		Gizmos.color = isActive ? Color.blue : Color.red;

		Gizmos.DrawWireCube(Vector3.zero + Vector3.forward * 0.5f, new Vector3(0.1f, 0.1f, 1f));

		Gizmos.matrix = Matrix4x4.identity;
		Gizmos.color = oldColor;

	}

	public void OnDrawGizmosSelected()
	{
		Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);

		Color oldColor = Gizmos.color;
		Gizmos.color = isActive ? Color.green : Color.magenta;

		Gizmos.DrawCube(Vector3.zero + Vector3.forward * 0.5f, new Vector3(0.1f, 0.1f, 1f));

		Gizmos.matrix = Matrix4x4.identity;
		Gizmos.color = oldColor;

	}
}
