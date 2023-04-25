using UnityEngine;
using UnityEngine.AI;

namespace FiniteStateMachine
{
    public class RoamState : StateMachine
    {
        private static readonly int IsWalking = Animator.StringToHash("isWalking");

        public RoamState(GameObject enemyGameObject,Stats enemyStats, NavMeshAgent agent, Animator myAnimator, Transform playerTransform)
            : base(enemyGameObject,enemyStats, agent, myAnimator, playerTransform)
        {
            Name = State.Roam;
        }

        protected override void Enter()
        {
            Agent.speed = EnemyStats.MoveSpeed;
            MyAnimator.SetTrigger(IsWalking);
            base.Enter();
        }

        protected override void Update()
        {
            if (Physics.SphereCast(Agent.transform.position, 30f, Vector3.zero, out RaycastHit hit) == PlayerTransform)
            {
                NextState = new PursueState(EnemyGameObject,EnemyStats,Agent,MyAnimator,PlayerTransform);
                Stage = Event.Exit;
            }
        }

        protected override void Exit()
        {
            MyAnimator.ResetTrigger(IsWalking);
            base.Exit();
        }
    }
}