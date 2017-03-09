using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class Shop : MonoBehaviour
{

    public Transform ShopTransform;
    public Object Prefab;

    private Dictionary<string, Transform> objects;
    public EntityEconomy Economy;

    void Start()
    {
        objects = new Dictionary<string, Transform>();
    }

    void Update()
    {
        var resources = Economy.GetResourceAmount();
        foreach(var resource in resources)
        {
            if(!objects.ContainsKey(resource.Key))
            {
                Transform obj = (Transform)Object.Instantiate(Prefab);
                obj.position = ShopTransform.position + new Vector3(Random.Range(10, 500), Random.Range(10, 500));
                objects.Add(resource.Key, obj);
            }
        }

        if(resources.Count < objects.Count)
        {
            Dictionary<string, Transform> objs = new Dictionary<string, Transform>();
            foreach(var obj in objects) {
                if(resources.ContainsKey(obj.Key))
                {
                    objs.Add(obj.Key, obj.Value);
                }
            }
            objects = objs;
        }
    }
}