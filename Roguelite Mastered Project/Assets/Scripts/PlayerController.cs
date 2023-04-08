using UnityEngine;

//1 Done - Use player SO to take values
//2 Done - Handle Movement 
//3 Done - Health regeneration
//4 Done - Loose Health
//------------------------------------------------------
//5 - Handle Dying

public class PlayerController : MonoBehaviour
{
    [SerializeField]private PlayerStats playerStats;
    
    private Rigidbody _myRigidbody;
    private Vector3 _movementInput;

    private void Start()
    {
        _myRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();

        playerStats.HealthRegeneration();
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            playerStats.TakeDamage(1);
        }
    }

    /// <summary>
    /// Handle Movement
    /// </summary>
    private void Move()
    {
        _movementInput = new Vector3(Input.GetAxis("Horizontal"),0f,Input.GetAxis("Vertical"));
        var moveVector = transform.TransformDirection(_movementInput) * playerStats.MoveSpeed;
        _myRigidbody.velocity = new Vector3(moveVector.x,_myRigidbody.velocity.y,moveVector.z);
    }
}
