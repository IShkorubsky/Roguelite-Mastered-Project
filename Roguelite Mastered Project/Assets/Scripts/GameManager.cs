using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public sealed class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    
    private GameObject _playerGameObject;
    
    public GameObject PlayerGameObject => _playerGameObject;

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

}