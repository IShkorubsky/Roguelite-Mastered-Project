using FiniteStateMachine;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _myAnimator;
    public Transform playerTransform;
    private StateMachine _currentState;
    
    // Start is called before the first frame update
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _myAnimator = GetComponent<Animator>();
        _currentState = new IdleState(gameObject,gameObject.GetComponent<Stats>(),_agent,_myAnimator,playerTransform);
    }

    // Update is called once per frame
    private void Update()
    {
        _currentState = _currentState.Process();
    }
}
