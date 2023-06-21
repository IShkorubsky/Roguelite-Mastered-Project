using UnityEngine;

public class DeactivateBullet : MonoBehaviour
{
    private float _timer;
    [SerializeField] private float timeBeforeDeactivation;

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
            Debug.Log("Entered");
            other.gameObject.GetComponent<EnemyAI>().enemyHealth -= 10;
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
