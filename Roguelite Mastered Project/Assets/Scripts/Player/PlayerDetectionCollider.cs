using UnityEngine;

namespace Player
{
    public class PlayerDetectionCollider : MonoBehaviour
    {
        [SerializeField] private EnemyAI enemyAI;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                enemyAI.targetInRange = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                enemyAI.targetInRange = false;
            }
        }
    }
}
