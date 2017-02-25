using UnityEngine;
using System.Collections;

public class Canon : MonoBehaviour
{
	public float reloadTime = 3f;
	public float force = 100;

	internal bool isReady = true;

	public void Shot()
	{
		isReady = false;
		StartCoroutine(Shooting());
	}

	IEnumerator Shooting()
	{
		yield return new WaitForSeconds(reloadTime);
		isReady = true;
	}

}
