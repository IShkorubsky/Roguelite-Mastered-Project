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
    [SerializeField] private Stats _chosenClass;
    private int _chosenClassInt;
    public int _enemiesToSpawn;
    public int _enemiesSpawned;
    
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
        _enemiesSpawning = true;
        _enemiesToSpawn = 3;
        _currentLevel = 0;
        Time.timeScale = 1;
        _gameOver = false;
        StartCoroutine(StartRound(1));
    }
    
    private void Update()
    {
        if (_enemiesSpawned == 0 && !_enemiesSpawning)
        {
            if (_currentLevel == 5)
            {
                UIManager.Instance.Victory();
            }
            StartCoroutine(StartRound(_currentLevel + 1));
        }
        
    }

    /// <summary>
    /// Starts level based on desired level index
    /// </summary>
    /// <param name="levelIndex"></param>
    private IEnumerator StartRound(int levelIndex)
    {
        _enemiesSpawning = true;
        _currentLevel = levelIndex;
        _enemiesToSpawn = _currentLevel * 2;
        for (int i = 0; i < _enemiesToSpawn; i++)
        {
            Debug.Log("Entered");
            _enemiesSpawned++;
            EnemySpawner.Instance.SpawnEnemy("Enemy");
            yield return new WaitForSeconds(2f);
        }
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
