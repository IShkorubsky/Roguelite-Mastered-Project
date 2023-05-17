using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectionCollider : MonoBehaviour
{
    [SerializeField] private EnemyAI enemyAI;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyAI.targetInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyAI.targetInRange = false;
        }
    }
}
