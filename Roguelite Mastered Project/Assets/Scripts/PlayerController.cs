using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

//1 Done - Use player SO to take values
//2 Done - Handle Movement 
//3 Done - Health regeneration
//4 Done - Loose Health
//5 Done - Handle Dying
//6 Done - Handle Dodging
//7 Done - Handle Attacking
//------------------------------------------------------

public class PlayerController : MonoBehaviour
{
    #region Variables

    [SerializeField] private Stats playerStats;
    [SerializeField] private Transform arrowSpawnPosition;
    public Stats PlayerStats => playerStats;
    [SerializeField] private DealDamage swordScript;
    [SerializeField] private Slider healthBar;
    
    private Camera _myCamera;
    private Rigidbody _myRigidbody;
    private Animator _myAnimator;
    
    private Vector2 _movementInput;
    private Vector3 _mousePosition;

    public bool _isDodging;
    private bool _isAttacking;
    private float _dodgeTimer;
    private float _attackTimer;
    private int _combo;

    private static readonly int IsDodging = Animator.StringToHash("isDodging");
    private static readonly int Combo = Animator.StringToHash("Combo");
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int IsIdle = Animator.StringToHash("isIdle");

    #endregion
    private void Start()
    {
        arrowSpawnPosition = transform.GetChild(1).GetComponent<Transform>();
        _myRigidbody = gameObject.GetComponent<Rigidbody>();
        _myAnimator = gameObject.GetComponent<Animator>();
        _myCamera = Camera.main;
        playerStats.SetMaxHealth();
        _dodgeTimer = 1f;
        _attackTimer = 0.8f;
    }

    private void Update()
    {
        
        Move();

        #region Attacking
        
        
        #endregion

        #region Health

        healthBar.value = playerStats.Health * 0.01f;
        //playerStats.HealthRegeneration();

        if (playerStats.Health <= 0)
        {
            //Handle death
            Debug.Log("Player Dead!");
        }

        #endregion
    }

    private void FixedUpdate()
    {
        #region Movement
        
        
        #endregion
    }

    #region Methods

     /// <summary>
     /// Handle Input
     /// </summary>
     public void OnMove(InputAction.CallbackContext context)
     {
         _movementInput = context.ReadValue<Vector2>();
     }

     /// <summary>
     /// Handles Player Movement
     /// </summary>
     private void Move()
     {
         var movement = new Vector3(_movementInput.x,0f,_movementInput.y);

         if (movement != Vector3.zero)
         {
             _myAnimator.ResetTrigger(IsIdle);
             _myAnimator.SetTrigger(IsRunning);
         }
         else
         {
             _myAnimator.ResetTrigger(IsRunning);
             _myAnimator.SetTrigger(IsIdle);
         }
         
         transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(movement),0.15f);
         
         transform.Translate(movement * (playerStats.MoveSpeed * Time.deltaTime),Space.World);
     }

    /// <summary>
    /// Handles dodging
    /// </summary>
    /// <returns></returns>
    private IEnumerator HandleDodging()
    {
        _isDodging = true;
        float startTimer = Time.time;

        while (Time.time < startTimer + _dodgeTimer)
        {
            _myAnimator.SetTrigger(IsDodging);
        }

        yield return new WaitForSeconds(_dodgeTimer);
        _isDodging = false;
    }

    /// <summary>
    /// Handles Melee Attacking
    /// </summary>
    /// <returns></returns>
    private IEnumerator MeleePlayerAttack()
    {
        float startTimer = Time.time;

        if (startTimer < _attackTimer)
        {
            _isAttacking = true;
            var direction = _mousePosition - transform.position;
            _combo++;
            _myAnimator.SetInteger(Combo, _combo);
            //_myRigidbody.AddForce(direction * (dashAmount * Time.fixedDeltaTime));
        }

        yield return new WaitForSeconds(_attackTimer);
        _isAttacking = false;
        _combo = 0;
        _myAnimator.SetInteger(Combo, _combo);
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
        bullet.GetComponent<Rigidbody>().AddForce(arrowSpawnPosition.forward * playerStats.RangedAttackSpeed,ForceMode.VelocityChange);
        yield break;
    }

    /// <summary>
    /// Activates collider on sword
    /// </summary>
    private void ActivateSword()
    {
        swordScript.ActivateCollider();
    }

    /// <summary>
    /// Deactivates collider on sword
    /// </summary>
    private void DeactivateSword()
    {
        swordScript.DeactivateCollider();
    }

    #endregion
}