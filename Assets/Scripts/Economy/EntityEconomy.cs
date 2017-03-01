using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityEconomy : MonoBehaviour {

	public int Money;

	protected List<Resource> Resources;

	// Use this for initialization
	void Start () {
		Resources = new List<Resource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
