using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerLocomotion : PlayerAnimator
{
    private Camera _myCamera;
    [SerializeField] private Slider dodgeCooldownBar;

    private Vector2 _movementInput;
    private Vector2 _mousePosition;
    private Vector3 _rotationTarget;

    private void OnEnable()
    {
        _myCamera = Camera.main;
    }

    private void Update()
    {
        var ray = _myCamera.ScreenPointToRay(_mousePosition);

        if (Physics.Raycast(ray, out var raycastHit))
        {
            _rotationTarget = raycastHit.point;
        }

        MoveWithAim();
    }

    #region Input Callbacks

    public void OnMove(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
    }

    public void OnMouseLook(InputAction.CallbackContext context)
    {
        _mousePosition = context.ReadValue<Vector2>();
    }

    public void OnLeftShift(InputAction.CallbackContext context)
    {
        IsDodging = context.ReadValueAsButton();
    }

    #endregion

    /// <summary>
    /// Handles moving while looking at the mouse position in the world
    /// </summary>
    private void MoveWithAim()
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

        if (movement != Vector3.zero)
        {
            ResetAnimatorTrigger(IsIdleHash);
            SetAnimatorTrigger(IsRunningHash);
        }
        else
        {
            ResetAnimatorTrigger(IsRunningHash);
            SetAnimatorTrigger(IsIdleHash);
        }

        if (dodgeCooldownBar != null)
        {
            if (dodgeCooldownBar.value >= 1)
            {
                dodgeCooldownBar.gameObject.SetActive(false);

                if (IsDodging)
                {
                    HandleDodging(movement);
                }
            }
            else
            {
                dodgeCooldownBar.gameObject.SetActive(true);
                dodgeCooldownBar.value += Time.deltaTime;
            }
        }
        else
        {
            dodgeCooldownBar = UIManager.Instance.DodgeCooldownSlider;
        }
        
        transform.Translate(movement * (GameManager.Instance.ChosenClass.MoveSpeed * Time.deltaTime), Space.World);
    }

    /// <summary>
    /// Handles dodging
    /// </summary>
    /// <returns></returns>
    private void HandleDodging(Vector3 movement)
    {
        transform.Translate(movement * (GameManager.Instance.ChosenClass.MoveSpeed * Time.deltaTime * 50),
            Space.World);
        dodgeCooldownBar.value = 0;
    }
}