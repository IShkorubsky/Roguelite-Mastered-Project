using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] private int attackDamage;

    private void Start()
    {
        attackDamage = gameObject.GetComponentInParent<PlayerController>().Stats.AttackDamage;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<PracticeDummy>().DummyStats.TakeDamage(attackDamage);
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
