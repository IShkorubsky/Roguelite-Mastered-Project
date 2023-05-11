using System;
using FiniteStateMachine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _myAnimator;
    public Transform playerTransform;
    private StateMachine _currentState;
    public float _distanceToPlayer;
    public float _enemyHealth;
    public bool playerInRange;
    
    [SerializeField] private Stats enemyStats;
    [SerializeField] private Slider healthSlider;

    public Stats EnemyStats => enemyStats;
    
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _myAnimator = GetComponent<Animator>();
        EnemyStats.SetMaxHealth();
        _enemyHealth = enemyStats.Health;
        healthSlider.maxValue = EnemyStats.MAXHealth;
    }

    private void Awake()
    {
        playerTransform = GameManager.Instance.PlayerGameObject.transform;
        _currentState = new IdleState(gameObject,EnemyStats,_agent,_myAnimator,playerTransform);
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            _distanceToPlayer = (playerTransform.position - transform.position).magnitude;

            if (_distanceToPlayer <= 3)
            {
                playerInRange = true;
            }
            else
            {
                playerInRange = false;
            }
        
            _currentState = _currentState.Process();
            Debug.Log(_currentState);

            healthSlider.value = _enemyHealth;

            if (_enemyHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            playerTransform = GameManager.Instance.PlayerGameObject.transform;
        }
    }

    public void GetDamaged()
    {
        _enemyHealth -= GameManager.Instance.ChosenClass.AttackDamage;
    }
}
