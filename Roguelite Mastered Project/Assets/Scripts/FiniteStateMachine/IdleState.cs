using UnityEngine;
using UnityEngine.AI;

namespace FiniteStateMachine
{
    public class IdleState : StateMachine
    {
        private static readonly int IsIdle = Animator.StringToHash("isIdle");

        public IdleState(GameObject enemyGameObject,Stats enemyStats, NavMeshAgent agent, Animator myAnimator, Transform playerTransform) : 
            base(enemyGameObject,enemyStats,agent,myAnimator,playerTransform)
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
            if (Random.Range(0, 100) < 10)
            {
                NextState = new RoamState(EnemyGameObject,EnemyStats, Agent, MyAnimator, PlayerTransform);
                Stage = Event.Exit;
            }
        }

        protected override void Exit()
        {
            MyAnimator.ResetTrigger(IsIdle);
            base.Exit();
        }
    }
}
