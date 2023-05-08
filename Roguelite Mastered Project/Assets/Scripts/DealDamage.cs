using System;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] private int attackDamage;

    private GameObject _enemyObject;

    private void Start()
    {
        attackDamage = GameController.Instance.ChosenClass.AttackDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyAI>().EnemyStats.TakeDamage(attackDamage);
        }
    }
}
