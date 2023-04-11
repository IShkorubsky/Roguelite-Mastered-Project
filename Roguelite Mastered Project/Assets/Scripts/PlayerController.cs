using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] private PlayerStats playerStats;
    public PlayerStats Stats => playerStats;
    [SerializeField] private DealDamage swordScript;
    [SerializeField] private Slider healthBar;
    
    private Camera _myCamera;
    private Rigidbody _myRigidbody;
    private Animator _myAnimator;
    
    private Vector3 _movementInput;
    private const float SmoothTime = 0.1f;

    private bool _isDodging;
    private float _dodgeTimer;
    private float _attackTimer;
    private int _combo;

    private static readonly int Running = Animator.StringToHash("Running");
    private static readonly int Dodging = Animator.StringToHash("Dodging");
    private static readonly int Combo = Animator.StringToHash("Combo");

    #endregion
    private void Start()
    {
        _myRigidbody = gameObject.GetComponent<Rigidbody>();
        _myAnimator = gameObject.GetComponent<Animator>();
        _myCamera = Camera.main;
        Stats.SetMaxHealth();
        _dodgeTimer = 1f;
        _attackTimer = 0.8f;
    }

    private void Update()
    {
        #region Movement
        
        if (!_isDodging)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (_movementInput.magnitude != 0)
                {
                    StartCoroutine(HandleDodging());
                }
            }

            Move();
        }
        
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            _myAnimator.SetBool(Running, false);
            _myRigidbody.velocity = Vector3.zero;
        }
        
        #endregion

        #region Attacking

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Look towards mouse position
            var ray = _myCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,out RaycastHit raycastHit))
            {
                //Rotate player model towards hit point
                transform.LookAt(new Vector3(raycastHit.point.x,0f,raycastHit.point.z));
                StartCoroutine(PlayerAttack());
            }
        }

        #endregion

        #region Health

        healthBar.value = Stats.Health * 0.01f;
        //playerStats.HealthRegeneration();

        //Test taking damage
        if (Input.GetKeyDown(KeyCode.K))
        {
            Stats.TakeDamage(5);
        }

        if (Stats.Health <= 0)
        {
            //Handle death
            Debug.Log("Player Dead!");
        }

        #endregion
    }

    #region Methods

     /// <summary>
    /// Handle Movement
    /// </summary>
    private void Move()
    {
        _movementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        if (_movementInput.magnitude > 0)
        {
            _myAnimator.SetBool(Running, true);
            RotateCharacter();
            var moveVector = transform.TransformDirection(Vector3.forward) * Stats.MoveSpeed;
            _myRigidbody.velocity = new Vector3(moveVector.x, _myRigidbody.velocity.y, moveVector.z);
        }

        if (_myRigidbody.velocity.magnitude == 0)
        {
            //bug happens when going the contrary way of what was previously going

            _myAnimator.SetBool(Running, false);
        }
    }

    /// <summary>
    /// Handles character rotation
    /// </summary>
    private void RotateCharacter()
    {
        var targetAngle = Mathf.Atan2(_movementInput.x, _movementInput.z) * Mathf.Rad2Deg;
        transform.rotation =
            Quaternion.Slerp(transform.rotation, Quaternion.Euler(0.0f, targetAngle, 0.0f), SmoothTime);
    }

    /// <summary>
    /// Handles dodging
    /// </summary>
    /// <returns></returns>
    private IEnumerator HandleDodging()
    {
        _isDodging = true;
        float timer = 0;
        if (timer < _dodgeTimer)
        {
            _myAnimator.SetTrigger(Dodging);
            var playerRollSpeed = Stats.MoveSpeed / 2;
            var dir = transform.forward * (playerRollSpeed) + Vector3.up * _myRigidbody.velocity.y;
            _myRigidbody.AddForce(dir, ForceMode.Impulse);
            yield return null;
        }

        while (timer < _dodgeTimer)
        {
            timer += Time.deltaTime;
        }

        yield return new WaitForSeconds(_dodgeTimer);
        _isDodging = false;
    }

    /// <summary>
    /// Handles Attacking
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayerAttack()
    {
        float timer = 0;

        if (timer < _attackTimer)
        {
            _combo++;
            _myAnimator.SetInteger(Combo, _combo);
        }

        while (timer < _dodgeTimer)
        {
            timer += Time.deltaTime;
        }

        yield return new WaitForSeconds(_attackTimer);
        _combo = 0;
        _myAnimator.SetInteger(Combo, _combo);
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