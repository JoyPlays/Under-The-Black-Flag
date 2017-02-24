using UnityEngine;

public class CodeDemo15 : MonoBehaviour 
{
	// Refs
	public Material Material;

	// Mono
	void Update () 
	{
		if (Material.HasProperty("_Reflectivity"))
		{
			Material.SetFloat("_Reflectivity", CodeDemoHelper.HelperTimeSin);
		}
	}
}
