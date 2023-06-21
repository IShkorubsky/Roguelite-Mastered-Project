using UnityEngine;

public class DealDamage : MonoBehaviour
{
    private GameObject _enemyObject;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyAI>().GetDamaged();
        }
    }
}
