using UnityEngine;

[ExecuteInEditMode]
public class ConnectHeightMap : MonoBehaviour 
{
	// Refs
	public AbstractHeightMapGenerator generator;
	public WaterShader waterScript;

	// Mono
	void Update ()
	{
		if (generator == null || waterScript == null)
		{
			Debug.LogWarning("ConnectHeightMap not setup: HeightMapGenerator " + (generator == null ? "null" : "ok") + ", WaterScript " + (waterScript == null ? "null" : "ok"));
			return;
		}
		waterScript.heightTexture = generator.GetHeightMap();
	}
}
