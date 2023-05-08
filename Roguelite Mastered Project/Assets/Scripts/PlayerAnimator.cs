using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _myAnimator;
    
    protected bool _isAttacking;
    protected bool _isDodging;

    protected static readonly int IsAttacking = Animator.StringToHash("isAttacking");
    protected static readonly int IsRunning = Animator.StringToHash("isRunning");
    protected static readonly int IsIdle = Animator.StringToHash("isIdle");

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