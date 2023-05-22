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
        IsAttackingMelee = context.ReadValueAsButton();
        SetAnimatorBool(IsAttackingHash,IsAttackingMelee);
    }

    public void OnRightMouseClick(InputAction.CallbackContext context)
    {
        IsAttackingRanged = context.ReadValueAsButton();
        SetAnimatorBool(IsAttackingHash,IsAttackingRanged);
    }
    
    public void ActivateSwordCollider()
    {
        swordCollider.gameObject.GetComponent<Collider>().enabled = true;
    }
    
    public void DeactivateSwordCollider()
    {
        swordCollider.gameObject.GetComponent<Collider>().enabled = false;
    }

    private void Update()
    {
        if (IsAttackingRanged)
        {
            RangedPlayerAttack();
        }
    }

    /// <summary>
    /// Handles Ranged Attacking
    /// </summary>
    /// <returns></returns>
    private void RangedPlayerAttack()
    {
        var bullet = Pool.Instance.Get("Bullet");
        bullet.transform.position = arrowSpawnPosition.transform.position;
        bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        bullet.SetActive(true);
        bullet.GetComponent<Rigidbody>().AddForce(arrowSpawnPosition.forward * GameManager.Instance.ChosenClass.RangedAttackSpeed,
            ForceMode.VelocityChange);
    }
}
