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
    private static Pool _instance;
    public List<PoolItem> poolItems;
    public List<GameObject> pooledItems;

    
    public static Pool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Pool();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }

    public GameObject Get(string myTag)
    {
        //If an item can be used the it will be returned to the asking method
        foreach (var item in pooledItems)
        {
            if (!item.activeInHierarchy && item.CompareTag(myTag))
            {
                return item;
            }
        }

        //If an item does not exists a new one is added to the pool
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
    
    private void Start()
    {
        //Creating the pool
        pooledItems = new List<GameObject>();
        
        foreach (var item in poolItems)
        {
            foreach (var objectItem in item.objectPrefab)
            {
                for (var i = 0; i < item.amount; i++)
                {
                    var obj = Instantiate(objectItem);
                    if (objectItem.CompareTag("Enemy"))
                    {
                        obj.GetComponent<EnemyAI>().playerTransform =
                            GameManager.Instance.playerGameObject.transform;
                    }
                    obj.SetActive(false);
                    pooledItems.Add(obj);
                }
            }
        }
    }
}
