using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PoolItem
{
    public GameObject objectPrefab;
    public int amount;
    public bool expandable;
}
public class Pool : MonoBehaviour
{
    public static Pool _instance;
    public List<PoolItem> items;
    public List<GameObject> pooledItems;

    private void Awake()
    {
        _instance = this;
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

        foreach (PoolItem item in items)
        {
            if (item.objectPrefab.CompareTag(myTag) && item.expandable)
            {
                var obj = Instantiate(item.objectPrefab);
                obj.SetActive(false);
                pooledItems.Add(obj);
                return obj;
            }
        }
        return null;
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        pooledItems = new List<GameObject>();
        foreach (var item in items)
        {
            for (var i = 0; i < item.amount; i++)
            {
                var obj = Instantiate(item.objectPrefab);
                obj.SetActive(false);
                pooledItems.Add(obj);
            }
        }

        foreach (var item in items)
        {
            
        }
    }
}
