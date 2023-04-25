using UnityEngine;
using UnityEngine.AI;

namespace FiniteStateMachine
{
    public class StateMachine
    {
        public enum State
        {
            Idle,
            Roam,
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

        protected State Name;
        protected StateMachine NextState;
        protected Event Stage;
    
        protected readonly GameObject EnemyGameObject;
        protected readonly Animator MyAnimator;
        protected readonly Transform PlayerTransform;
        protected readonly NavMeshAgent Agent;

        public StateMachine(GameObject enemyGameObject, NavMeshAgent agent, Animator myAnimator, Transform playerTransform)
        {
            EnemyGameObject = enemyGameObject;
            Agent = agent;
            MyAnimator = myAnimator;
            PlayerTransform = playerTransform;
            
            Stage = Event.Enter;
        }

        public virtual void Enter()
        {
            Stage = Event.Update;
        }

        public virtual void Update()
        {
            Stage = Event.Update;
        }

        public virtual void Exit()
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
            var direction = PlayerTransform.position - EnemyGameObject.transform.position;
            if (direction.magnitude < Agent.GetComponent<PlayerStats>().AttackRange)
            {
                return true;
            }

            return false;
        }
    }
}
