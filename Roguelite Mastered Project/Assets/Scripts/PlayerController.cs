using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
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
    private const float SmoothTime = 0.1f;
    [SerializeField] private float dashAmount;
    [SerializeField] private float dashTimer;

    public bool _isDodging;
    private bool _isAttacking;
    private float _dodgeTimer;
    private float _attackTimer;
    private int _combo;

    private static readonly int Running = Animator.StringToHash("Running");
    private static readonly int Dodging = Animator.StringToHash("Dodging");
    private static readonly int Combo = Animator.StringToHash("Combo");
    private static readonly int IsRunning = Animator.StringToHash("isRunning");

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
        #region Movement

        _movementInput = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")).normalized;

        #endregion
        
        #region Attacking

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Look towards mouse position
            StartCoroutine(MeleePlayerAttack());
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            StartCoroutine(RangedPlayerAttack());
        }

        #endregion

        #region Health

        healthBar.value = playerStats.Health * 0.01f;
        //playerStats.HealthRegeneration();

        //Test taking damage
        if (Input.GetKeyDown(KeyCode.K))
        {
            playerStats.TakeDamage(5);
        }

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
        Move();
        
        if (!_isDodging)
        {
            LookTowardsMouse();
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                //Bug here
                StartCoroutine(HandleDodging());
            }
        }

        #endregion
    }

    #region Methods

     /// <summary>
     /// Handle Input
     /// </summary>
     private void OnMove(InputAction.CallbackContext context)
     {
         _movementInput = context.ReadValue<Vector2>();
     }

     private void Move()
     {
         var movement = new Vector3(_movementInput.x,0f,_movementInput.y);
         
         transform.Translate(movement * (playerStats.MoveSpeed * Time.fixedDeltaTime),Space.World);
     }

     private void LookTowardsMouse()
    {
        var ray = _myCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray: ray, hitInfo: out RaycastHit hit))
        {
            if (hit.collider.transform.CompareTag("Ground"))
            {
                transform.LookAt(new Vector3(hit.point.x,0f,hit.point.z));
                _mousePosition = hit.point;
            }
        }
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
            _myAnimator.SetTrigger(Dodging);
            StartCoroutine(Move(_mousePosition.normalized * (dashAmount * Time.deltaTime)));
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

        if (startTimer < _attackTimer + dashAmount)
        {
            _isAttacking = true;
            var direction = _mousePosition - transform.position;
            _combo++;
            _myAnimator.SetInteger(Combo, _combo);
            _myRigidbody.AddForce(direction * (dashAmount * Time.fixedDeltaTime));
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