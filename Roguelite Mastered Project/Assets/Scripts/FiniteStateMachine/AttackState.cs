using UnityEngine;
using UnityEngine.AI;

namespace FiniteStateMachine
{
    public class AttackState : StateMachine
    {
        private const float RotationSpeed = 2.0f;
        private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

        public AttackState(GameObject enemyGameObject,Stats enemyStats, NavMeshAgent agent, Animator myAnimator, Transform playerTransform)
            : base(enemyGameObject,enemyStats, agent, myAnimator, playerTransform)
        {
            Name = State.Attack;
        }

        protected override void Enter()
        {
            MyAnimator.SetTrigger(IsAttacking);
            Agent.isStopped = true;
            base.Enter();
        }

        protected override void Update()
        {
            var direction = PlayerTransform.position - EnemyGameObject.transform.position;
            direction.y = 0;
            
            EnemyGameObject.transform.rotation = Quaternion.Slerp(EnemyGameObject.transform.rotation,Quaternion.LookRotation(direction),Time.deltaTime * RotationSpeed );

            if (!CanAttackPlayer())
            {
                NextState = new IdleState(EnemyGameObject,EnemyStats,Agent,MyAnimator,PlayerTransform);
                Stage = Event.Exit;
            }
        }

        protected override void Exit()
        {
            MyAnimator.ResetTrigger(IsAttacking);
            base.Exit();
        }
    }
}
