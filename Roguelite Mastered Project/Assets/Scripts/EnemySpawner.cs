using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private static EnemySpawner _instance;
    public List<Transform> spawnPoints;

    public static EnemySpawner Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EnemySpawner();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    
    public void SpawnEnemies(string enemyTag)
    {
        foreach (var spawnPoint in spawnPoints)
        {
            var spawnedEnemy = Pool.Instance.Get(enemyTag);
            spawnedEnemy.SetActive(true);
            spawnedEnemy.transform.position = spawnPoint.transform.position;
        }
    }
}
