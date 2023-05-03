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
    private Vector2 _mousePosition;
    private Vector3 _rotationTarget;

    public bool _isDodging;
    private bool _isAttacking;
    private float _dodgeTimer;
    private int _combo;

    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int IsIdle = Animator.StringToHash("isIdle");
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");
    private static readonly int IsDodging = Animator.StringToHash("isDodging");

    #endregion

    private void Start()
    {
        arrowSpawnPosition = transform.GetChild(1).GetComponent<Transform>();
        _myRigidbody = gameObject.GetComponent<Rigidbody>();
        _myAnimator = gameObject.GetComponent<Animator>();
        _myCamera = Camera.main;
        playerStats.SetMaxHealth();
        _dodgeTimer = 1f;
    }

    private void Update()
    {
        var ray = _myCamera.ScreenPointToRay(_mousePosition);

        if (Physics.Raycast(ray, out var raycastHit))
        {
            _rotationTarget = raycastHit.point;
        }

        MoveWithAim();

        if (_isAttacking)
        {
            Debug.Log("isAttacking");
            MeleePlayerAttack();
        }
        
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

    #region Methods

    #region Input Callbacks

    public void OnMove(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
    }

    public void OnMouseLook(InputAction.CallbackContext context)
    {
        _mousePosition = context.ReadValue<Vector2>();
    }

    public void OnLeftMouseClick(InputAction.CallbackContext context)
    {
        _isAttacking = context.ReadValueAsButton();
    }

    #endregion

    #region Actions

    /// <summary>
    /// Handles moving while looking at the mouse position in the world
    /// </summary>
     public void MoveWithAim()
    {
        var lookPosition = _rotationTarget - transform.position;
        lookPosition.y = 0;
        var rotation = Quaternion.LookRotation(lookPosition);

        var aimDirection = new Vector3(_rotationTarget.x, 0f, _rotationTarget.z);
        var distanceToMouse = _mousePosition - new Vector2(transform.position.x,transform.position.y);

        if (aimDirection != Vector3.zero && distanceToMouse.magnitude > 3f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);
        }

        var movement = new Vector3(_movementInput.x, 0f, _movementInput.y);

        if (!_isAttacking)
        {
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
        }

        transform.Translate(movement * (playerStats.MoveSpeed * Time.deltaTime), Space.World);
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
    private void MeleePlayerAttack()
    {
        _myAnimator.ResetTrigger(IsIdle);
        _myAnimator.ResetTrigger(IsRunning);
        _myAnimator.SetTrigger(IsAttacking);
        _isAttacking = false;
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

    #endregion

   

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