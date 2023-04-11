using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//1 Done - Use player SO to take values
//2 Done - Handle Movement 
//3 Done - Health regeneration
//4 Done - Loose Health
//5 Done - Handle Dying
//6 Done - Handle Dodging
//------------------------------------------------------

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Slider healthBar;
    
    private Rigidbody _myRigidbody;
    private Animator _myAnimator;
    private Vector3 _movementInput;
    private const float SmoothTime = 0.1f;
    
    private bool _isDodging;
    private float _dodgeTimer;
    
    private static readonly int Running = Animator.StringToHash("Running");
    private static readonly int Dodging = Animator.StringToHash("Dodging");

    private void Start()
    {
        _myRigidbody = gameObject.GetComponent<Rigidbody>();
        _myAnimator = gameObject.GetComponent<Animator>();
        playerStats.SetMaxHealth();
        _dodgeTimer = 1f;
    }

    private void Update()
    {
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
            _myAnimator.SetBool(Running,false);
            _myRigidbody.velocity = Vector3.zero;
        }
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
    }

    /// <summary>
    /// Handle Movement
    /// </summary>
    private void Move()
    {
        _movementInput = new Vector3(Input.GetAxis("Horizontal"),0f,Input.GetAxis("Vertical"));
        
        if (_movementInput.magnitude > 0)
        {
            _myAnimator.SetBool(Running,true);
            RotateCharacter();
            var moveVector = transform.TransformDirection(Vector3.forward) * playerStats.MoveSpeed;
            _myRigidbody.velocity = new Vector3(moveVector.x,_myRigidbody.velocity.y,moveVector.z);
        }
        
        if(_myRigidbody.velocity.magnitude == 0)
        {
            //bug happens when going the contrary way of what was previously going
            
            _myAnimator.SetBool(Running,false);
        }
    }

    private void RotateCharacter()
    {
        var targetAngle = Mathf.Atan2(_movementInput.x, _movementInput.z) * Mathf.Rad2Deg;
        transform.rotation =  Quaternion.Slerp(transform.rotation,Quaternion.Euler(0.0f,targetAngle,0.0f),SmoothTime);
    }

    private IEnumerator HandleDodging()
    {
        _isDodging = true;
        float timer = 0;
        if (timer < _dodgeTimer)
        {
            _myAnimator.SetTrigger(Dodging);
            var playerRollSpeed = playerStats.MoveSpeed / 2;
            var dir = transform.forward * (playerRollSpeed) + Vector3.up * _myRigidbody.velocity.y;
            _myRigidbody.AddForce(dir,ForceMode.Impulse);
            yield return null;
        }

        while (timer < _dodgeTimer)
        {
            timer += Time.deltaTime;
        }
        
        yield return new WaitForSeconds(_dodgeTimer);
        _isDodging = false;
    }
}
