using UnityEngine;
using UnityEngine.AI;

namespace FiniteStateMachine
{
    public class AttackState : StateMachine
    {
        public AttackState(GameObject enemyGameObject, NavMeshAgent agent, Animator myAnimator, Transform playerTransform)
            : base(enemyGameObject, agent, myAnimator, playerTransform)
        {
            Name = State.Roam;
        }
    }
}
