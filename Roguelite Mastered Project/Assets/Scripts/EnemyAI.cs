using System;
using FiniteStateMachine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _myAnimator;
    public Transform targetTransform;
    private StateMachine _currentState;
    public float _distanceToTarget;
    public float _enemyHealth;
    public bool targetInRange;
    
    [SerializeField] private Stats enemyStats;
    [SerializeField] private Slider healthSlider;

    public Stats EnemyStats => enemyStats;

    private void Awake()
    {
        targetTransform = GameObject.FindGameObjectWithTag("EnemyTarget").transform;
        _agent = GetComponent<NavMeshAgent>();
        _myAnimator = GetComponent<Animator>();
        
        EnemyStats.SetMaxHealth();
        _enemyHealth = enemyStats.Health;
        healthSlider.maxValue = EnemyStats.MAXHealth;
    }

    private void Update()
    {
        if (targetTransform != null)
        {
            _distanceToTarget = (targetTransform.position - transform.position).magnitude;

            if (_distanceToTarget <= 4)
            {
                targetInRange = true;
            }
            else
            {
                targetInRange = false;
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
            targetTransform = GameManager.Instance.PlayerGameObject.transform;
        }
    }

    public void GetDamaged()
    {
        _enemyHealth -= GameManager.Instance.ChosenClass.AttackDamage;
    }

    public void Spawn()
    {
        _currentState = new IdleState(gameObject,enemyStats,_agent,_myAnimator,targetTransform);
    }
}
