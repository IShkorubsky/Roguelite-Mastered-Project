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

    public void SpawnEnemy(string enemyTag)
    {
        var spawnedEnemy = Pool.Instance.Get(enemyTag);
        var randomSpawnPoint = spawnPoints[Random.Range(0, 3)];
        spawnedEnemy.SetActive(true);
        spawnedEnemy.GetComponent<EnemyAI>().SetIdleState();
        spawnedEnemy.GetComponent<EnemyAI>()._myAnimator.runtimeAnimatorController =
            spawnedEnemy.GetComponent<Animator>().runtimeAnimatorController;
        spawnedEnemy.transform.position = randomSpawnPoint.transform.position;
    }
}