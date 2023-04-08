using UnityEngine;

//1 Done - Use player SO to take values
//2 Done - Handle Movement 
//3 Done - Health regeneration
//4 Done - Loose Health
//5 Done - Handle Dying
//------------------------------------------------------

public class PlayerController : MonoBehaviour
{
    [SerializeField]private PlayerStats playerStats;
    
    private Rigidbody _myRigidbody;
    private Animator _myAnimator;
    private Vector3 _movementInput;
    private float smoothTime = 0.1f;
    
    private static readonly int Running = Animator.StringToHash("Running");

    private void Start()
    {
        _myRigidbody = gameObject.GetComponent<Rigidbody>();
        _myAnimator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        Move();

        Debug.Log(_myRigidbody.velocity);
        //playerStats.HealthRegeneration();
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            playerStats.TakeDamage(1);
        }

        if (playerStats.Health <= 0)
        {
            //Handle death
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
        else
        {
            //bug happens when going the contrary way of what was previously going
            
            _myAnimator.SetBool(Running,false);
        }
    }

    private void RotateCharacter()
    {
        var targetAngle = Mathf.Atan2(_movementInput.x, _movementInput.z) * Mathf.Rad2Deg;
        transform.rotation =  Quaternion.Slerp(transform.rotation,Quaternion.Euler(0.0f,targetAngle,0.0f),smoothTime);
    }
}
