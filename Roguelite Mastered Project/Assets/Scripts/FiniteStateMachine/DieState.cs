using UnityEngine;
using UnityEngine.AI;

namespace FiniteStateMachine
{
    public class DieState : StateMachine
    {
        public DieState(GameObject enemyGameObject, NavMeshAgent agent, Animator myAnimator, Transform playerTransform)
            : base(enemyGameObject, agent, myAnimator, playerTransform)
        {
            Name = State.Roam;
        }
    }
}
