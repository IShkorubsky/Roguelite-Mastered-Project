using UnityEngine;
using UnityEngine.AI;

namespace FiniteStateMachine
{
    public class PursueState : StateMachine
    {
        public PursueState(GameObject enemyGameObject, NavMeshAgent agent, Animator myAnimator, Transform playerTransform)
            : base(enemyGameObject, agent, myAnimator, playerTransform)
        {
            Name = State.Roam;
        }
    }
}
