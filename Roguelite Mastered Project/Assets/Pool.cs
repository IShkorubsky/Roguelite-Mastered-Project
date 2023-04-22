using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PoolItem
{
    public GameObject objectPrefab;
    public int amount;
}
public class Pool : MonoBehaviour
{
    private static Pool _instance;
    public List<PoolItem> items;
    public List<GameObject> pooledItems;

    private void Awake()
    {
        _instance = this;
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
    }
}
