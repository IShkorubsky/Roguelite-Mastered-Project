using UnityEngine;
using UnityEngine.AI;

namespace FiniteStateMachine
{
    public class PursueState : StateMachine
    {
        private static readonly int IsRunning = Animator.StringToHash("isRunning");

        public PursueState(GameObject enemyGameObject,Stats enemyStats, NavMeshAgent agent, Animator myAnimator, Transform playerTransform)
            : base(enemyGameObject,enemyStats, agent, myAnimator, playerTransform)
        {
            Name = State.Pursue;
        }

        protected override void Enter()
        {
            MyAnimator.SetTrigger(IsRunning);
            base.Enter();
        }

        protected override void Update()
        {
            Agent.speed = EnemyStats.MoveSpeed;
            Agent.isStopped = false;
            Agent.SetDestination(PlayerTransform.position);
            if (Agent.hasPath)
            {
                if (CanAttackPlayer())
                {
                    NextState = new AttackState(EnemyGameObject,EnemyStats,Agent,MyAnimator,PlayerTransform);
                    Stage = Event.Exit;
                }
            }
        }

        protected override void Exit()
        {
            MyAnimator.ResetTrigger(IsRunning);
            base.Exit();
        }
    }
}
