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
		//Debug.Log("Adding resource: " + name + " value: " + value);
		foreach(Resource res in Resources) {
			if(res.Name.ToString() == name) {
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
		//Debug.Log("Using resource: " + name + " value: " + value);
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

	public int GetResourceAmount(string resource)
	{
		var economy = GetResourceAmount();
		if (economy != null && economy.ContainsKey(resource))
		{
			return economy[resource];
		}

		return 0;

	}


	public Dictionary<string, int> GetResourceAmount()
    {
        Dictionary<string, int> response = new Dictionary<string, int>();
        foreach(Resource res in Resources)
        {
            if(res.Ammount > 0)
            {
                response.Add(res.Name, res.Ammount);
            }
        }
        return response;
    }

	public int GetResourceSellPrices(string resource)
	{
		var economy = GetResourceSellPrices();
		if (economy != null && economy.ContainsKey(resource))
		{
			return economy[resource];
		}

		return 0;
	}

	public Dictionary<string, int> GetResourceSellPrices() { //player buys from entity
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
			//int ammount = res.Ammount;
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

	public int GetResourceBuyPrices(string resource)
	{
		var economy = GetResourceBuyPrices();
		if (economy != null && economy.ContainsKey(resource))
		{
			return economy[resource];
		}

		return 0;
	}

	public Dictionary<string, int> GetResourceBuyPrices() { //Player sels to Entity
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
			Debug.Log(res.Name);
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

    public bool BuyResource(string name, int price_per_one, int value)
    {
        if(Money < price_per_one * value)
        {
            return false;
        }
        foreach (Resource res in Resources)
        {
            if (res.Name == name)
            {
                res.Ammount += value;
                Money -= value * price_per_one;
                return true;
            }
        }

        Resource r = new Resource();
        r.Name = name;
        r.Ammount = value;
        Money -= value * price_per_one;

        Resources.Add(r);
        return true;

    }

	public bool SellResource(string name, int price_per_one, int value) {
        foreach(Resource res in Resources) {
            if(res.Name == name)
            {
                if(res.Ammount < value)
                {
                    return false;
                }
                res.Ammount -= value;
                Money += value * price_per_one;
                return true;
            }
        }
        return false;
	}

}
