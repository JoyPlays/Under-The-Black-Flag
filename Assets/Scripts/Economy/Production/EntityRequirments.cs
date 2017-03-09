using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityRequirments : ResourceList {

	private float Counter = 0;
	private EntityEconomy Economy;
	public int Interval = 10;

	// Use this for initialization
	void Start () {
		Economy = GetComponent<EntityEconomy>();
	}
	
	// Update is called once per frame
	void Update () {
		Counter += Time.deltaTime;

		if(Counter >= Interval) {
			Counter = 0;
			foreach(Resource res in Resources) {
				Economy.RemoveResource(res.Name, res.Ammount);
			}
		} 
	}
}
