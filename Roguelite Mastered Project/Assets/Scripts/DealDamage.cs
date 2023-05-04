using System;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] private int attackDamage;
    [SerializeField] private PlayerController playerController;
    private DateTime _nextDamage;
    public float _damageAfterTime;
    private bool _enemyInFightRange;

    private GameObject _enemyObject;

    private void Start()
    {
        attackDamage = gameObject.GetComponentInParent<PlayerController>().PlayerStats.AttackDamage;
    }

    private void Awake()
    {
        _nextDamage = DateTime.Now;
    }

    private void FixedUpdate()
    {
        if (_enemyInFightRange)
        {
            DamageEnemy();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && playerController.IsAttackingBool)
        {
            _enemyObject = other.gameObject;
            _enemyInFightRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _enemyInFightRange = false;
        }
    }

    private void DamageEnemy()
    {
        if (_nextDamage <= DateTime.Now)
        {
            _enemyObject.GetComponent<EnemyAI>().EnemyStats.TakeDamage(attackDamage);

            _nextDamage = DateTime.Now.AddSeconds(Convert.ToDouble(_damageAfterTime));
        }
    }
}
