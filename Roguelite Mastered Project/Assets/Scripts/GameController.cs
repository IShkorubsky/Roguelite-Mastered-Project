using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    [SerializeField] private Stats[] classes;
    [SerializeField] private Stats _chosenClass;
    private int _chosenClassInt;
    
    private bool _gameOver;
    private GameObject _playerGameObject;
    
    public Stats ChosenClass => _chosenClass;

    public static GameController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameController();
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
}
