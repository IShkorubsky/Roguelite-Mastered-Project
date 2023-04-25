using UnityEngine;
using UnityEngine.AI;

namespace FiniteStateMachine
{
    public class RoamState : StateMachine
    {
        private static readonly int IsWalking = Animator.StringToHash("isWalking");

        public RoamState(GameObject enemyGameObject, NavMeshAgent agent, Animator myAnimator, Transform playerTransform)
            : base(enemyGameObject, agent, myAnimator, playerTransform)
        {
            Name = State.Roam;
        }

        public override void Enter()
        {
            MyAnimator.SetTrigger(IsWalking);
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
            MyAnimator.ResetTrigger(IsWalking);
            base.Exit();
        }
    }
}