using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CreateLevel",fileName = "New Level")]
public class LevelSO : ScriptableObject
{
    [SerializeField] private GameObject gameWorld;
    [SerializeField] private int levelIndex;
    [SerializeField] private Transform playerSpawnPosition;
    [SerializeField] private List<Transform> enemySpawnPositions;

    public GameObject GameWorld => gameWorld;

    public int LevelIndex => levelIndex;

    public Transform PlayerSpawnPosition => playerSpawnPosition;

    public List<Transform> EnemySpawnPositions => enemySpawnPositions;

    private void OnEnable()
    {
        playerSpawnPosition = gameWorld.transform.GetChild(0).GetComponent<Transform>();
        
        foreach (var enemySpawnPosition in gameWorld.transform.GetChild(1).gameObject.GetComponentsInChildren<Transform>())
        {
            if (enemySpawnPosition.CompareTag("EnemySpawnPosition"))
            {
                enemySpawnPositions.Add(enemySpawnPosition);
            }
        }
    }
}
