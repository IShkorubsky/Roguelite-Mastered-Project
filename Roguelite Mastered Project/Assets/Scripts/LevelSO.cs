using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CreateLevel",fileName = "New Level")]
public class LevelSO : ScriptableObject
{
    [SerializeField] private GameObject gameWorld;
    [SerializeField] private int levelIndex;
    [SerializeField] private Transform playerSpawnPosition;
    [SerializeField] private List<Transform> enemySpawnPositions;
}
