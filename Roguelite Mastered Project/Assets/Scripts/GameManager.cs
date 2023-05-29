using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    [SerializeField] private Stats[] classes;
    //[SerializeField] private LevelSO[] levels;
    //[SerializeField] private GameObject playerPrefab;
    public GameObject playerGameObject;
    [SerializeField] private Transform gameWorldSpawnPosition;
    
    private int _currentLevel;
    private Stats _chosenClass;
    private int _chosenClassInt;
    public int _numberOfEnemies;
    
    private bool _gameOver;
    private bool _enemiesSpawning;
    //private GameObject _currentGameWorld;
    
    public Stats ChosenClass => _chosenClass;
    public int CurrentLevel => _currentLevel;
    public GameObject PlayerGameObject => playerGameObject;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }
    }
    
    private void Awake()
    {
        _instance = this;
    }
    
    private void Start()
    {
        _numberOfEnemies = 3;
        _currentLevel = 0;
        Time.timeScale = 1;
        _gameOver = false;
        _chosenClassInt = 0;
        _chosenClass = classes[_chosenClassInt];
        StartRound(6);
    }
    
    private void Update()
    {
        if (_enemiesSpawning)
        {
            return;
        }
    }

    /// <summary>
    /// Starts level based on desired level index
    /// </summary>
    /// <param name="levelIndex"></param>
    private void StartRound(int levelIndex)
    {
        _currentLevel = levelIndex;
        for (int i = 0; i <= _numberOfEnemies; i++)
        {
            EnemySpawner.Instance.SpawnEnemy("Enemy");
        }
        _numberOfEnemies = _currentLevel * 2;
    }
    
    /*
    /// <summary>
    /// Spawns The player in the world
    /// </summary>
    private void SpawnPlayer()
    {
        playerGameObject = Instantiate(playerPrefab, gameWorldSpawnPosition.position, Quaternion.identity);
        playerGameObject.transform.position = levels[_currentLevel].PlayerSpawnPosition.position;
    }
    */
}
