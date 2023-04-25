using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] private int attackDamage;

    private void Start()
    {
        attackDamage = gameObject.GetComponentInParent<PlayerController>().PlayerStats.AttackDamage;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyAI>().EnemyStats.TakeDamage(attackDamage);
        }
    }

    public void ActivateCollider()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    public void DeactivateCollider()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
