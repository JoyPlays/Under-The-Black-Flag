using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityProduction : ResourceList {

	public int SimulateIntervals = 2;
	private float Counter = 0;
	private EntityEconomy Economy;
	public int Interval = 10;

	void Start () {
		Economy = GetComponent<EntityEconomy>();
		foreach(Resource res in Resources) {
			Economy.AddResource(res.Name, res.Ammount * SimulateIntervals);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Counter += Time.deltaTime;

		if(Counter >= Interval) {
			Counter = 0;
			foreach(Resource res in Resources) {
				Economy.AddResource(res.Name, res.Ammount);
			}
		} 
	}
}
