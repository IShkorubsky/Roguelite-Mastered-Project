using FiniteStateMachine;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _myAnimator;
    public Transform playerTransform;
    public StateMachine _currentState;
    private Stats _enemyStats;
    
    // Start is called before the first frame update
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _myAnimator = GetComponent<Animator>();
        _enemyStats = GetComponent<PracticeDummy>().DummyStats;
        _currentState = new IdleState(gameObject,_enemyStats,_agent,_myAnimator,playerTransform);
    }

    // Update is called once per frame
    private void Update()
    {
        _currentState = _currentState.Process();
        Debug.Log(_currentState);
    }
}
