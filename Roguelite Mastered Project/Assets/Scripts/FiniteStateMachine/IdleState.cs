using UnityEngine;
using UnityEngine.AI;

namespace FiniteStateMachine
{
    public class IdleState : StateMachine
    {
        private static readonly int IsIdle = Animator.StringToHash("isIdle");

        public IdleState(GameObject enemyGameObject,Stats enemyStats, NavMeshAgent agent, Animator myAnimator, Transform targetTransform) : 
            base(enemyGameObject,enemyStats,agent,myAnimator,targetTransform)
        {
            Name = State.Idle;
        }

        protected override void Enter()
        {
            MyAnimator.SetTrigger(IsIdle);
            base.Enter();
        }

        protected override void Update()
        {
            NextState = new PursueState(EnemyGameObject,EnemyStats, Agent, MyAnimator, TargetTransform);
            Stage = Event.Exit;
        }

        protected override void Exit()
        {
            MyAnimator.ResetTrigger(IsIdle);
            base.Exit();
        }
    }
}
