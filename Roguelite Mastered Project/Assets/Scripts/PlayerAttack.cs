using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerAttack : Player
{
    [SerializeField] private Transform arrowSpawnPosition;
    [SerializeField] private Collider damageCollider;

    private bool _isAttacking;

    public bool IsAttackingBool => _isAttacking;

    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

    private void OnEnable()
    {
        arrowSpawnPosition = transform.GetChild(1).GetComponent<Transform>();
    }

    public void OnLeftMouseClick(InputAction.CallbackContext context)
    {
        _isAttacking = context.ReadValueAsButton();
        SetAnimatorBool(IsAttacking,_isAttacking);
    }
    
    public void ActivateAttackCollider()
    {
        damageCollider.gameObject.SetActive(true);
    }
    
    public void DeactivateAttackCollider()
    {
        damageCollider.gameObject.SetActive(false);
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
        bullet.GetComponent<Rigidbody>().AddForce(arrowSpawnPosition.forward * playerStats.RangedAttackSpeed,
            ForceMode.VelocityChange);
        yield break;
    }
}
