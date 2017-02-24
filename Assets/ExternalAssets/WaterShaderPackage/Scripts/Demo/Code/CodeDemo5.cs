using UnityEngine;

public class CodeDemo5 : MonoBehaviour 
{
	// Refs
	public Material Material;

	// Mono
	void Update () 
	{
		if (Material.HasProperty("_NormalStrength"))
		{
			Material.SetFloat("_NormalStrength", CodeDemoHelper.HelperTimeNormalized);
		}
	}
}
