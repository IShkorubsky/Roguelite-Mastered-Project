using UnityEngine;
using UnityEngine.AI;

namespace FiniteStateMachine
{
    public class StateMachine
    {
        protected enum State
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
        protected Event Stage;
        protected StateMachine NextState;
        
        protected readonly GameObject EnemyGameObject;
        protected readonly Stats EnemyStats;
        protected readonly Animator MyAnimator;
        protected readonly Transform TargetTransform;
        protected readonly NavMeshAgent Agent;

        protected StateMachine(GameObject enemyGameObject, Stats enemyStats,NavMeshAgent agent, Animator myAnimator, Transform targetTransform)
        {
            EnemyGameObject = enemyGameObject;
            EnemyStats = enemyStats;
            Agent = agent;
            MyAnimator = myAnimator;
            TargetTransform = targetTransform;
            
            Stage = Event.Enter;
        }

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
            var direction = TargetTransform.position - EnemyGameObject.transform.position;
            if (direction.magnitude < EnemyStats.AttackRange)
            {
                return true;
            }

            return false;
        }
        
        public bool CanSeePlayer()
        {
            var direction = TargetTransform.position - EnemyGameObject.transform.position;
            var angle = Vector3.Angle(direction, EnemyGameObject.transform.forward);
            if (direction.magnitude < EnemyStats.AttackRange * 2 && angle < 30)
            {
                return true;
            }

            return false;
        }
    }
}
