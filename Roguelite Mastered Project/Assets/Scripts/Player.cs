using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [SerializeField] protected Stats playerStats;
    public Stats PlayerStats => playerStats;

    private Animator _myAnimator;
    
    private void Start()
    {
        _myAnimator = gameObject.GetComponent<Animator>();
    }

    protected void SetAnimatorTrigger(int paramHash)
    {
        _myAnimator.SetTrigger(paramHash);
    }
    
    protected void SetAnimatorBool(int paramHash,bool state)
    {
        _myAnimator.SetBool(paramHash,state);
    }
}