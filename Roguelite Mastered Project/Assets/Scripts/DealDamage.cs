using System;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] private int attackDamage;
    [SerializeField] private Player playerController;

    private GameObject _enemyObject;

    private void Start()
    {
        attackDamage = gameObject.GetComponentInParent<Player>().PlayerStats.AttackDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyAI>().EnemyStats.TakeDamage(attackDamage);
        }
    }
}
