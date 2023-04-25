using UnityEngine;
using UnityEngine.AI;

namespace FiniteStateMachine
{
    public class IdleState : StateMachine
    {
        private static readonly int IsIdle = Animator.StringToHash("isIdle");

        public IdleState(GameObject enemyGameObject, NavMeshAgent agent, Animator myAnimator, Transform playerTransform) : 
            base(enemyGameObject,agent,myAnimator,playerTransform)
        {
            Name = State.Idle;
        }
        
        public override void Enter()
        {
            MyAnimator.SetTrigger(IsIdle);
            base.Enter();
        }

        public override void Update()
        {
            if (Random.Range(0, 100) < 10)
            {
                NextState = new RoamState(EnemyGameObject, Agent, MyAnimator, PlayerTransform);
                Stage = Event.Exit;
            }
        }

        public override void Exit()
        {
            MyAnimator.ResetTrigger(IsIdle);
            base.Exit();
        }
    }
}
