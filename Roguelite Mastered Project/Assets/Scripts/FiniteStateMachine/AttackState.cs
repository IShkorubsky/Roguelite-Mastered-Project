using UnityEngine;
using UnityEngine.AI;

namespace FiniteStateMachine
{
    public class AttackState : StateMachine
    {
        private const float RotationSpeed = 2.0f;
        private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

        public AttackState(GameObject enemyGameObject,Stats enemyStats, NavMeshAgent agent, Animator myAnimator, Transform targetTransform)
            : base(enemyGameObject,enemyStats, agent, myAnimator, targetTransform)
        {
            Name = State.Attack;
        }

        protected override void Enter()
        {
            MyAnimator.SetTrigger(IsAttacking);
            Agent.isStopped = true;
        }

        protected override void Update()
        {
            var direction = TargetTransform.position - EnemyGameObject.transform.position;
            direction.y = 0;
            EnemyGameObject.transform.rotation = Quaternion.Slerp(
                EnemyGameObject.transform.rotation,Quaternion.LookRotation(direction),Time.deltaTime * RotationSpeed );
        }

        protected override void Exit()
        {
            MyAnimator.ResetTrigger(IsAttacking);
            base.Exit();
        }
    }
}
