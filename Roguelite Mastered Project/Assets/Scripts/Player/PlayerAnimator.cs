using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _myAnimator;
    
    protected bool IsAttackingMelee;
    protected bool IsAttackingRanged;
    protected bool IsDodging;

    protected static readonly int IsAttackingHash = Animator.StringToHash("isAttacking");
    protected static readonly int IsRunningHash = Animator.StringToHash("isRunning");
    protected static readonly int IsIdleHash = Animator.StringToHash("isIdle");

    private void Start()
    {
        _myAnimator = gameObject.GetComponent<Animator>();
    }

    protected void SetAnimatorTrigger(int paramHash)
    {
        _myAnimator.SetTrigger(paramHash);
    }
    
    protected void ResetAnimatorTrigger(int paramHash)
    {
        _myAnimator.ResetTrigger(paramHash);
    }
    
    protected void SetAnimatorBool(int paramHash,bool state)
    {
        _myAnimator.SetBool(paramHash,state);
    }
}