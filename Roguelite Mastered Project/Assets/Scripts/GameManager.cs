using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    [SerializeField] private Stats[] classes;
    [SerializeField] private LevelSO[] levels;
    [SerializeField] private GameObject playerPrefab;
    public GameObject playerGameObject;
    [SerializeField] private Transform gameWorldSpawnPosition;
    
    private int _currentLevel;
    private Stats _chosenClass;
    private int _chosenClassInt;
    
    private bool _gameOver;
    private GameObject _currentGameWorld;
    
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
        StartLevel(0);
    }
    
    private void Update()
    {
        if (_gameOver)
        {
            return;
        }
    }

    /// <summary>
    /// Starts level based on desired level index
    /// </summary>
    /// <param name="levelIndex"></param>
    private void StartLevel(int levelIndex)
    {
        _currentGameWorld = Instantiate(levels[levelIndex].GameWorld, gameWorldSpawnPosition.position, Quaternion.identity);
        EnemySpawner.Instance.spawnPoints = levels[levelIndex].EnemySpawnPositions;
        SpawnPlayer();
        EnemySpawner.Instance.SpawnEnemies("Enemy");
        _currentLevel = levelIndex;
    }
    
    /// <summary>
    /// Spawns The player in the world
    /// </summary>
    private void SpawnPlayer()
    {
        playerGameObject = Instantiate(playerPrefab, gameWorldSpawnPosition.position, Quaternion.identity);
        playerGameObject.transform.position = levels[_currentLevel].PlayerSpawnPosition.position;
    }
}
