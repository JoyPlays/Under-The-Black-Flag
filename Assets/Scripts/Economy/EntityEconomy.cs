using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityEconomy : MonoBehaviour {

	public int Money;

	protected List<Resource> Resources = new List<Resource>();

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddResource(string name, int value) {
		Debug.Log("Adding resource: " + name + " value: " + value);
		foreach(Resource res in Resources) {
			if(res.Name == name) {
				res.Ammount += value;
				return;
			}
		}

		Resource resource = new Resource();
		resource.Name = name;
		resource.Ammount = value;
		Resources.Add(resource);
	}

	public void RemoveResource(string name, int value) {
		Debug.Log("Using resource: " + name + " value: " + value);
			foreach(Resource res in Resources) {
				if(res.Name == name) {
					res.Ammount -= value;
					if(res.Ammount < 0) {
						res.Ammount = 0;
					}
					return;
				}
			}
	}

	public Dictionary<string, int> GetResourceSellPrices() {
		EntityRequirments req = GetComponent<EntityRequirments>();
		Dictionary<string, int> response = new Dictionary<string, int>();

		Dictionary<string, int> cycle_difference = new Dictionary<string, int>();
		foreach(Resource res in req.Resources) {
			if(res.Ammount < 1) {
				continue;
			}
			response.Add(res.Name, res.BasePrice);
			cycle_difference.Add(res.Name, -res.Ammount);
		}

		EntityProduction prod = GetComponent<EntityProduction>();
		foreach(Resource res in prod.Resources) {
			if(cycle_difference.ContainsKey(res.Name)) {
				cycle_difference[res.Name] += res.Ammount;
			} else {
				cycle_difference.Add(res.Name, res.Ammount);
			}
		}

		


		foreach(Resource res in Resources) {
			int ammount = res.Ammount;
			int diffenrece = cycle_difference[res.Name];
			int base_price = response[res.Name];

			if(diffenrece < 0) { //TODO: Interpolate exponentialy based on ammount;
				base_price = base_price * Random.Range(30, 50);
			} else if(diffenrece == 0) {
				base_price = base_price * Random.Range(15, 20);
			} else {
				base_price = base_price + Random.Range(-2, 2);
			}

			response[res.Name] = base_price;
		}
		
		return response;
	}

	public Dictionary<string, int> GetResourceBuyPrices() {
		EntityRequirments req = GetComponent<EntityRequirments>();
		Dictionary<string, int> response = new Dictionary<string, int>();

		Dictionary<string, int> cycle_difference = new Dictionary<string, int>();
		foreach(Resource res in req.Resources) {
			if(res.Ammount < 1) {
				continue;
			}
			response.Add(res.Name, res.BasePrice);
			cycle_difference.Add(res.Name, -res.Ammount);
		}

		EntityProduction prod = GetComponent<EntityProduction>();
		foreach(Resource res in prod.Resources) {
			if(cycle_difference.ContainsKey(res.Name)) {
				cycle_difference[res.Name] += res.Ammount;
			} else {
				cycle_difference.Add(res.Name, res.Ammount);
			}
		}

		


		foreach(Resource res in Resources) {
			int ammount = res.Ammount;
			int diffenrece = cycle_difference[res.Name];
			int base_price = response[res.Name];

			if(ammount == 0) {
				if(diffenrece < 0) {
					base_price = base_price * Random.Range(80, 100);
				} else if(diffenrece == 0) {
					base_price = base_price * Random.Range(50, 70);
				} else {
					base_price = base_price + Random.Range(30, 40);
				}
			} else {
				if(diffenrece < 0) { 
					base_price = base_price * Random.Range(20, 25);
				} else if(diffenrece == 0) {
					base_price = base_price * Random.Range(15, 20);
				} else {
					base_price = base_price * Random.Range(-5, 0);
				}
			}
		}
		
		return response;
	}

	public void BuyResource(string name, int value) {

	}

	public void SellResource(string name, int value) {

	}

}
