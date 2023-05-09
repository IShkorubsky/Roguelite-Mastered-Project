using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    [SerializeField] private Stats[] classes;
    [SerializeField] private LevelSO[] levels;
    [SerializeField] private Transform gameWorldSpawnPosition;
    
    private int _currentLevel;
    private Stats _chosenClass;
    private int _chosenClassInt;
    
    private bool _gameOver;
    private GameObject _playerGameObject;
    private GameObject _currentGameWorld;
    
    public Stats ChosenClass => _chosenClass;
    public int CurrentLevel => _currentLevel;

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
    }
    
    private void Update()
    {
        if (_gameOver)
        {
            return;
        }
    }

    private void StartLevel(int levelIndex)
    {
        _currentGameWorld = Instantiate(levels[levelIndex].GameWorld, gameWorldSpawnPosition.position, Quaternion.identity);
        EnemySpawner.Instance.spawnPoints = levels[levelIndex].EnemySpawnPositions;
        _playerGameObject.transform.position = levels[levelIndex].PlayerSpawnPosition.position;
        _currentLevel = levelIndex;
    }
}
