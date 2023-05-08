using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerAttack : PlayerAnimator
{
    [SerializeField] private Transform arrowSpawnPosition;
    [SerializeField] private Collider swordCollider;
    
    private void OnEnable()
    {
        arrowSpawnPosition = transform.GetChild(1).GetComponent<Transform>();
    }

    public void OnLeftMouseClick(InputAction.CallbackContext context)
    {
        IsAttacking = context.ReadValueAsButton();
        SetAnimatorBool(IsAttackingHash,IsAttacking);
    }
    
    public void ActivateSwordCollider()
    {
        swordCollider.gameObject.GetComponent<Collider>().enabled = true;
    }
    
    public void DeactivateSwordCollider()
    {
        swordCollider.gameObject.GetComponent<Collider>().enabled = false;
    }
    
    /// <summary>
    /// Handles Ranged Attacking
    /// </summary>
    /// <returns></returns>
    private IEnumerator RangedPlayerAttack()
    {
        var bullet = Pool._instance.Get("Bullet");
        bullet.transform.position = arrowSpawnPosition.transform.position;
        bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        bullet.SetActive(true);
        bullet.GetComponent<Rigidbody>().AddForce(arrowSpawnPosition.forward * GameManager.Instance.ChosenClass.RangedAttackSpeed,
            ForceMode.VelocityChange);
        yield break;
    }
}
