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
    private int _numberOfWaves;
    
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
        _numberOfWaves = 3;
        _currentLevel = 0;
        Time.timeScale = 1;
        _gameOver = false;
        _chosenClassInt = 0;
        _chosenClass = classes[_chosenClassInt];
    }
    
    private void Update()
    {
        if (!_enemiesSpawning)
        {
            StartRound(_currentLevel + 1);
        }
        
        if (!_gameOver)
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
        //_currentGameWorld = Instantiate(levels[levelIndex].GameWorld, gameWorldSpawnPosition.position, gameWorldSpawnPosition.rotation);
        //EnemySpawner.Instance.spawnPoints = levels[levelIndex].EnemySpawnPositions;
        //SpawnPlayer();
        _currentLevel = levelIndex;
        _numberOfWaves = _currentLevel * 2;
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        _enemiesSpawning = true;
        for (int i = 0; i < _numberOfWaves; i++)
        {
            EnemySpawner.Instance.SpawnEnemies("Enemy");
        }
        yield return new WaitForSeconds(3f);
        _enemiesSpawning = false;
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
