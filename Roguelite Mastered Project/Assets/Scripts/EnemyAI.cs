using FiniteStateMachine;
using Player;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    public Animator myAnimator;
    public Transform targetTransform;
    private StateMachine _currentState;
    private float _distanceToTarget;
    public float enemyHealth;
    public bool targetInRange;
    
    [SerializeField] private Stats enemyStats;
    [SerializeField] private Slider healthSlider;

    public Stats EnemyStats => enemyStats;

    private void Awake()
    {
        targetTransform = GameObject.FindGameObjectWithTag("EnemyTarget").transform;
        gameObject.GetComponent<ParticleSystem>().Play();
        _agent = GetComponent<NavMeshAgent>();
        myAnimator = GetComponent<Animator>();
        EnemyStats.SetMaxHealth();
        enemyHealth = enemyStats.Health;
        healthSlider.maxValue = EnemyStats.MAXHealth;
    }

    private void Update()
    {
        healthSlider.gameObject.transform.LookAt(Camera.main.transform);
        _distanceToTarget = (targetTransform.position - transform.position).magnitude;
        
        if (targetTransform != null)
        {
            if (_distanceToTarget <= EnemyStats.AttackRange)
            {
                _currentState = new AttackState(gameObject,enemyStats,_agent,myAnimator,targetTransform);
                targetInRange = true;
            }
            else
            {
                targetInRange = false;
            }
        
            _currentState = _currentState.Process();

            healthSlider.value = enemyHealth;

            if (enemyHealth <= 0)
            {
                GameManager.Instance.enemiesSpawned--;
                SetIdleState();
                gameObject.SetActive(false);
                enemyHealth = EnemyStats.MAXHealth;
            }
        }
        else
        {
            targetTransform = GameManager.Instance.PlayerGameObject.transform;
        }
    }

    public void GetDamaged()
    {
        enemyHealth -= GameManager.Instance.ChosenClass.AttackDamage;
    }

    public void SetIdleState()
    {
        _currentState = new IdleState(gameObject,enemyStats,_agent,myAnimator,targetTransform);
    }

    public void DamageHouse()
    {
        PlayerHealth.GetDamaged(enemyStats.AttackDamage);
    }
}
