using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CannonParams
{
	public string name;
	public int cost;
}

public class CannonData : ScriptableObject
{
	public List<CannonParams> cannons;
}
