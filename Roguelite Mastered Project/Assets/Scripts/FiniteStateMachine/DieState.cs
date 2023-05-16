using UnityEngine;
using UnityEngine.AI;

namespace FiniteStateMachine
{
    public class DieState : StateMachine
    {
        public DieState(GameObject enemyGameObject,Stats enemyStats, NavMeshAgent agent, Animator myAnimator, Transform targetTransform)
            : base(enemyGameObject,enemyStats, agent, myAnimator, targetTransform)
        {
            Name = State.Die;
        }

        protected override void Enter()
        {
            base.Enter();
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override void Exit()
        {
            base.Exit();
        }
    }
}
