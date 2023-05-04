using System;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] private int attackDamage;
    [SerializeField] private PlayerController playerController;

    private GameObject _enemyObject;

    private void Start()
    {
        attackDamage = gameObject.GetComponentInParent<PlayerController>().PlayerStats.AttackDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyAI>().EnemyStats.TakeDamage(attackDamage);
        }
    }
}
