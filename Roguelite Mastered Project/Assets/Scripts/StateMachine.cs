using UnityEngine;
using UnityEngine.AI;

public class StateMachine
{
    public enum State
    {
        Idle,
        Pursue,
        Attack,
        Die
    }

    protected enum Event
    {
        Enter,
        Update,
        Exit
    }

    public StateMachine Name;
    protected StateMachine NextState;
    protected Event Stage;
    
    protected readonly GameObject Enemy;
    protected readonly Animator MyAnimator;
    protected readonly Transform PlayerTransform;
    protected readonly NavMeshAgent Agent;

    protected virtual void Enter()
    {
        Stage = Event.Update;
    }

    protected virtual void Update()
    {
        Stage = Event.Update;
    }

    protected virtual void Exit()
    {
        Stage = Event.Exit;
    }

    public StateMachine Process()
    {
        if (Stage == Event.Enter)
        {
            Enter();
        }

        if (Stage == Event.Update)
        {
            Update();
        }
        
        if (Stage == Event.Exit)
        {
            Exit();
            return NextState;
        }

        return this;
    }

    public bool CanAttackPlayer()
    {
        var direction = PlayerTransform.position - Enemy.transform.position;
        if (direction.magnitude < Agent.GetComponent<PlayerStats>().AttackRange)
        {
            return true;
        }

        return false;
    }
}
