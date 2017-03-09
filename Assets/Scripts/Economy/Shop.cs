using UnityEngine;

public enum ShopResources
{
	Wood
}

public class Shop : MonoBehaviour
{

	public Transform[] objects;
	public EntityEconomy economy;

    void Start()
    {
        
    }

	void Update()
	{
		var resources = economy.GetResourceAmount();
		foreach(Transform trans in objects)
		{
			if(!resources.ContainsKey(trans.name))
			{
				trans.gameObject.SetActive(false);
				continue;
			}
			if(resources[trans.name] > 0)
			{
				trans.gameObject.SetActive(true);
			}
			else
			{
				trans.gameObject.SetActive(false);
			}
		}
	}
}