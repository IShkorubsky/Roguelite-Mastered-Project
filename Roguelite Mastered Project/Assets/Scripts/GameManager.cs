using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    
    private bool _gameOver;

    private GameObject _playerGameObject;
    public GameObject PlayerGameObject => _playerGameObject;
    
    private static GameManager Instance()
    {
        // Uses lazy initialization.
        // Note: this is not thread safe.
        if (_instance == null)
        {
            _instance = new GameManager();
        }
        return _instance;
    }
    
    private void Start()
    {
        Time.timeScale = 1;
        _gameOver = false;
    }
    
    private void Update()
    {
        if (_gameOver)
        {
            return;
        }
    }
}
