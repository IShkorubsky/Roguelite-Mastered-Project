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
    
    private bool _gameOver;
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
        _currentLevel = 0;
        Time.timeScale = 1;
        _gameOver = false;
        _chosenClassInt = 0;
        _chosenClass = classes[_chosenClassInt];
        StartCoroutine(SpawnRound());
    }
    
    private void Update()
    {
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
        EnemySpawner.Instance.SpawnEnemies("Enemy");
        _currentLevel = levelIndex;
    }

    private IEnumerator SpawnRound()
    {
        for (int i = 0; i < _currentLevel; i++)
        {
            EnemySpawner.Instance.SpawnEnemies("Enemy");
        }
        yield return new WaitForSeconds(2f);
        StartRound(_currentLevel + 1);
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
