using System;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] private int attackDamage;

    private GameObject _enemyObject;
    
    public static event Action OnDamageTaken;

    private void Start()
    {
        attackDamage = GameManager.Instance.ChosenClass.AttackDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            OnDamageTaken?.Invoke();
        }
    }
}
