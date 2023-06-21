using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    [SerializeField] private Stats[] classes;
    public GameObject playerGameObject;
    [SerializeField] private Transform gameWorldSpawnPosition;
    
    private int _currentLevel;
    [SerializeField] private Stats chosenClass;
    private int _chosenClassInt;
    private int _enemiesToSpawn;
    public int enemiesSpawned;
    
    private bool _enemiesSpawning;
    public Stats ChosenClass => chosenClass;
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
        chosenClass = classes[0];
        _enemiesSpawning = true;
        _enemiesToSpawn = 3;
        _currentLevel = 0;
        Time.timeScale = 1;
        StartCoroutine(StartRound(1));
    }
    
    private void Update()
    {
        if (enemiesSpawned == 0 && !_enemiesSpawning)
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
            enemiesSpawned++;
            EnemySpawner.Instance.SpawnEnemy("Enemy");
            yield return new WaitForSeconds(2f);
        }
        _enemiesSpawning = false;
    }
}
