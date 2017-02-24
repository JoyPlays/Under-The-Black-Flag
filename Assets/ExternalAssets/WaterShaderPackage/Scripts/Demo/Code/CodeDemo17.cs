using UnityEngine;

public class CodeDemo17 : MonoBehaviour 
{
	// Refs
	public WaterShader WaterShaderScript;

	// Mono
	void Update ()
	{
		WaterShaderScript.reflectSky = CodeDemoHelper.HelperTimeSin > 0;
	}
}
