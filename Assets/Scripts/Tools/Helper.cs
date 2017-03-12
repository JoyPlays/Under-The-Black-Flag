using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Helper
{
	public static T GetRandomEnum<T>()
	{
		System.Array A = System.Enum.GetValues(typeof(T));
		T V = (T) A.GetValue(UnityEngine.Random.Range(0, A.Length));
		return V;
	}

	public static T FindClosestTarget<T>(Vector3 position, float distance = Mathf.Infinity) where T : MonoBehaviour
	{
		T[] objs = GameObject.FindObjectsOfType<T>();
		T closest = null;
		float dist = Mathf.Infinity;

		foreach (var obj in objs)
		{
			float delta = Vector3.Distance(position, obj.transform.position);

			if (delta <= distance && delta < dist)
			{
				closest = obj;
				dist = delta;
			}
		}

		return closest;
	}

	public static float AngleInRad(Vector3 vec1, Vector3 vec2)
	{
		return Mathf.Atan2(vec2.x - vec1.x, vec2.z - vec1.z);
	}

	public static float AngleInDeg(Vector3 vec1, Vector3 vec2, float delta = 0)
	{
		float angle = AngleInRad(vec1, vec2) * 180f / Mathf.PI + delta;
		
		int an = Mathf.FloorToInt(Mathf.Abs(angle) / 360f)*360;
		if (angle < 0) angle = an + 360 + angle;
		if (angle > 360) angle = an - angle;

		return angle;
	}

	public static bool GetMouseOnGround(Collider ground, out Vector3 position, float grid = 0)
	{
		position = Vector3.zero;
		if (!ground)
		{
			Debug.LogError("Hit on ground without collider");
			return false;
		}

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (ground.Raycast(ray, out hit, Mathf.Infinity))
		{
			Vector3 pos = hit.point;
			if (grid > 0)
			{
				pos.x = Mathf.Floor(pos.x/grid)*grid;
				pos.z = Mathf.Floor(pos.z/grid)*grid;
			}
			position = pos;
			return true;
		}
		return false;
	}

	public static void SwitchColliders(this GameObject obj, bool active)
	{
		Collider[] colliders = obj.GetComponents<Collider>();
		foreach (Collider collider in colliders)
		{
			collider.enabled = active;
		}

		colliders = obj.GetComponentsInChildren<Collider>();
		foreach (Collider collider in colliders)
		{
			collider.enabled = active;
		}
	}

}
/*
 * LAYER masks
 layerMask = 1 << LayerMask.NameToLayer ("layerX"); // only check for collisions with layerX
 
 layerMask = ~(1 << LayerMask.NameToLayer ("layerX")); // ignore collisions with layerX
 
 LayerMask layerMask = ~(1 << LayerMask.NameToLayer ("layerX") | 1 << LayerMask.NameToLayer ("layerY")); // ignore both layerX and layerY
 */


public class WeightClass<T>
{
	public float Weight { get; set; }
	public T Value { get; set; }
}

public static class Weighted
{
	public static float TotalWeight = 0;

	public static WeightClass<T> Create<T>(float weight, T value)
	{
		TotalWeight += weight;
		return new WeightClass<T> { Weight = weight, Value = value };
	}

	//private static readonly System.Random random = new System.Random();

	public static T GetWeighted<T>(this IEnumerable<WeightClass<T>> collection)
	{
		//var rnd = random.NextDouble();
		//Debug.Log(rnd);

		float total = 0;

		float random = UnityEngine.Random.Range(0, TotalWeight + 0.00f);
		foreach (var item in collection)
		{
			total += item.Weight;
			if (total >= random)
			{
				return item.Value;
			}
		}
		throw new InvalidOperationException("The proportions in the collection do not add up to 1.");
	}
}

public struct IntVector
{
	public int x;
	public int y;

	
	public IntVector(int x, int y) : this()
	{
		this.x = x;
		this.y = y;
	}
}
