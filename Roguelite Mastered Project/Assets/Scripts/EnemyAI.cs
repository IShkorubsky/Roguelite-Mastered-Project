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
    public bool playerInRange;
    
    [SerializeField] private Stats enemyStats;
    [SerializeField] private Slider healthSlider;

    public Stats EnemyStats => enemyStats;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _myAnimator = GetComponent<Animator>();
        _currentState = new IdleState(gameObject,EnemyStats,_agent,_myAnimator,playerTransform);
        
        EnemyStats.SetMaxHealth();
        healthSlider.maxValue = EnemyStats.MAXHealth;
    }

    private void Update()
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

        healthSlider.value = EnemyStats.Health;

        if (EnemyStats.Health <= 0)
        {
            Destroy(gameObject);
        }
        
    }
}
