using UnityEngine;
using UnityEngine.AI;

namespace FiniteStateMachine
{
    public class PursueState : StateMachine
    {
        private static readonly int IsRunning = Animator.StringToHash("isRunning");
        private static EnemyAI _enemyAI;

        public PursueState(GameObject enemyGameObject,Stats enemyStats, NavMeshAgent agent, Animator myAnimator, Transform targetTransform)
            : base(enemyGameObject,enemyStats, agent, myAnimator, targetTransform)
        {
            Name = State.Pursue;
            _enemyAI = EnemyGameObject.GetComponent<EnemyAI>();
        }

        protected override void Enter()
        {
            Agent.isStopped = false;
            Agent.speed = EnemyStats.MoveSpeed;
            MyAnimator.SetTrigger(IsRunning);
            base.Enter();
        }

        protected override void Update()
        {
            if (!Agent.hasPath && !_enemyAI.targetInRange)
            {
                Agent.isStopped = false;
                Agent.SetDestination(TargetTransform.position);
            }
        }

        protected override void Exit()
        {
            MyAnimator.ResetTrigger(IsRunning);
            base.Exit();
        }
    }
}
