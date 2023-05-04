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

    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider dodgeCooldownBar;

    private Camera _myCamera;
    private Rigidbody _myRigidbody;
    private Animator _myAnimator;
    [SerializeField] private Collider damageCollider;

    private Vector2 _movementInput;
    private Vector2 _mousePosition;
    private Vector3 _rotationTarget;

    private bool _isDodging;
    private bool _isAttacking;
    public bool IsAttackingBool => _isAttacking;

    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int IsIdle = Animator.StringToHash("isIdle");
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

    #endregion

    private void Start()
    {
        arrowSpawnPosition = transform.GetChild(1).GetComponent<Transform>();
        _myRigidbody = gameObject.GetComponent<Rigidbody>();
        _myAnimator = gameObject.GetComponent<Animator>();
        _myCamera = Camera.main;
        playerStats.SetMaxHealth();
    }

    private void Update()
    {
        var ray = _myCamera.ScreenPointToRay(_mousePosition);

        if (Physics.Raycast(ray, out var raycastHit))
        {
            _rotationTarget = raycastHit.point;
        }

        MoveWithAim();

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
        _myAnimator.SetBool(IsAttacking,_isAttacking);
    }
    
    public void OnLeftShift(InputAction.CallbackContext context)
    {
        _isDodging = context.ReadValueAsButton();
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
        var distanceToMouse = _mousePosition - new Vector2(transform.position.x, transform.position.y);

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

        if (dodgeCooldownBar.value >= 1)
        {
            dodgeCooldownBar.gameObject.SetActive(false);

            if (_isDodging)
            {
                HandleDodging(movement);
            }
        }
        else
        {
            dodgeCooldownBar.gameObject.SetActive(true);
            dodgeCooldownBar.value += Time.deltaTime;
        }
        
        transform.Translate(movement * (playerStats.MoveSpeed * Time.deltaTime), Space.World);
    }

    /// <summary>
    /// Handles dodging
    /// </summary>
    /// <returns></returns>
    private void HandleDodging(Vector3 movement)
    {
        transform.Translate(movement * (playerStats.MoveSpeed * Time.deltaTime * 50), Space.World);
        dodgeCooldownBar.value = 0;
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

    #endregion

    #endregion
}