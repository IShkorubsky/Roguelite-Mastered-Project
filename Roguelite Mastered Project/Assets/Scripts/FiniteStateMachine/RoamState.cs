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

        public static Vector3 RandomNavSphere (Vector3 origin, float distance, int layermask) {
            var randomDirection = Random.insideUnitSphere * distance;
           
            randomDirection += origin;

            NavMesh.SamplePosition (randomDirection, out var navHit, distance, layermask);
           
            return navHit.position;
        }
        
        protected override void Enter()
        {
            Agent.speed = EnemyStats.MoveSpeed;
            MyAnimator.SetTrigger(IsWalking);
            base.Enter();
        }

        protected override void Update()
        {
            Agent.SetDestination(RandomNavSphere(EnemyGameObject.transform.position,EnemyStats.AttackRange * 2,-1));
            if (Agent.hasPath)
            {
                if (Agent.remainingDistance < 0.1f)
                {
                    
                }
            }
        }

        protected override void Exit()
        {
            MyAnimator.ResetTrigger(IsWalking);
            base.Exit();
        }
    }
}