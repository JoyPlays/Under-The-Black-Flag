using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour
{

	public Camera lookAtCamera;

	// Update is called once per frame
	void Update()
	{
		transform.LookAt(lookAtCamera.transform);
	}
}
