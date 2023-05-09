using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PoolItem
{
    public GameObject[] objectPrefab;
    public int amount;
    public bool expandable;
}

public class Pool : MonoBehaviour
{
    public static Pool Instance;
    public List<PoolItem> poolItems;
    public List<GameObject> pooledItems;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject Get(string myTag)
    {
        foreach (var item in pooledItems)
        {
            if (!item.activeInHierarchy && item.CompareTag(myTag))
            {
                return item;
            }
        }

        foreach (PoolItem item in poolItems)
        {
            foreach (var objectItem in item.objectPrefab)
            {
                for (var i = 0; i < item.amount; i++)
                {
                    var obj = Instantiate(objectItem);
                    obj.SetActive(false);
                    pooledItems.Add(obj);
                }
            }
        }
        return null;
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        pooledItems = new List<GameObject>();
        
        foreach (var item in poolItems)
        {
            foreach (var objectItem in item.objectPrefab)
            {
                for (var i = 0; i < item.amount; i++)
                {
                    var obj = Instantiate(objectItem);
                    obj.SetActive(false);
                    pooledItems.Add(obj);
                }
            }
        }
    }
}
