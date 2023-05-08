using System;
using UnityEngine;

public class DeactivateBullet : MonoBehaviour
{
    private float _timer;
    [SerializeField] private float timeBeforeDeactivation;
    private PlayerAnimator _playerController;

    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnimator>();
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            BulletTimer(timeBeforeDeactivation);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyAI>().EnemyStats.TakeDamage(GameController.Instance.ChosenClass.AttackDamage);
            _timer = 0;
            Deactivate();
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void BulletTimer(float timeBeforeInactive)
    {
        if (_timer < timeBeforeInactive)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            _timer = 0;
            Deactivate();
        }
    }
}
