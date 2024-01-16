using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public bool CanMove { get; private set; } = true;
    private bool _isSprinting => _canSprint && Input.GetKey(_sprintKey);
    private bool _shouldJump => Input.GetKeyDown(_jumpKey) && _characterController.isGrounded;

    [Header("Functional Options")]
    [SerializeField] private bool _canSprint = true;
    [SerializeField] private bool _canJump = true;
    [SerializeField] private bool _canUseHeadBob = true;
    [SerializeField] private bool _useFootsteps = true;
    [SerializeField] private bool _canInteract = true;

    [Header("Controls")]
    [SerializeField] private KeyCode _sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;

    [Header("Movement Parametrs")]
    [SerializeField] private float _movementSpeed = 3.0f;
    [SerializeField] private float _sprintSpeed = 6.0f;

    [Header("Jumping Parametrs")]
    [SerializeField] private float _jumpForce = 8.0f;
    [SerializeField] private float _gravity = 30.0f;

    [Header("Interaction")]
    [SerializeField] private Vector3 interactionRayPoint = default;
    [SerializeField] private Image interactionPointer;
    [SerializeField] private float rayDistance = 1;
    private Color whiteColor;
    private Color greenColor;
    

    [Header("Headbob Parametrs")]
    [SerializeField] private float _walkBobSpeed = 14f;
    [SerializeField] private float _walkBobAmount = 0.05f;
    [SerializeField] private float _sprintBobSpeed = 18f;
    [SerializeField] private float _sprintBobAmount = 0.11f;
    private float _defaultYPos = 0;
    private float _timer;

    [Header("Camera Parametrs")]
    [SerializeField, Range(1, 10)] private float _cameraSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float _cameraSpeedY = 2.0f;
    [SerializeField, Range(1, 100)] private float _upCameraLimit = 80.0f;
    [SerializeField, Range(1, 100)] private float _downCameraLimit = 80.0f;

    [Header("Footsteps Parametrs")]
    [SerializeField] private float _baseStepSpeed = 0.5f;
    [SerializeField] private float _sprintStepMultipler = 0.6f;
    [SerializeField] private AudioSource _footstepAudioSource = default;
    [SerializeField] private AudioClip[] _stepClips = default;
    private float _footstepTimer = 0f;
    private float _currentOffset => _isSprinting ? _baseStepSpeed * _sprintStepMultipler : _baseStepSpeed;

    private Camera _playerCamera;
    private CharacterController _characterController;

    private Vector3 _moveDirection;
    private Vector2 _currentInput;

    private float _rotationX = 0;

    private void Awake()
    {
        if (!this.enabled)
            return;

        _playerCamera = GetComponentInChildren<Camera>();
        _characterController = GetComponent<CharacterController>();
        _defaultYPos = _playerCamera.transform.localPosition.y;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SetColor();
    }

    private void Update()
    {
        if (CanMove)
        {
            HandleMovementInput();
            HandleMouseLook();

            if (_canJump)
                HandleJump();

            if (_canUseHeadBob)
                HandleHeadBob();

            if (_useFootsteps)
                HandleFootsteps();

            if (_canInteract)
                HandleInteraction();

            ApplyFinalMovements();
        }
    }

    private void HandleMovementInput()
    {
        _currentInput = new Vector2((_isSprinting ? _sprintSpeed : _movementSpeed) * Input.GetAxis("Vertical"),
            (_isSprinting ? _sprintSpeed : _movementSpeed) * Input.GetAxis("Horizontal"));

        float moveDirectionY = _moveDirection.y;
        _moveDirection = (transform.TransformDirection(Vector3.forward) * _currentInput.x)
            + (transform.TransformDirection(Vector3.right) * _currentInput.y);
        _moveDirection.y = moveDirectionY;
    }

    private void HandleMouseLook()
    {
        _rotationX -= Input.GetAxis("Mouse Y") * _cameraSpeedY;
        _rotationX = Mathf.Clamp(_rotationX, -_upCameraLimit, _downCameraLimit);
        _playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * _cameraSpeedX, 0);
    }

    private void ApplyFinalMovements()
    {
        if (!_characterController.isGrounded)
        {
            _moveDirection.y -= _gravity * Time.deltaTime;
        }

        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    private void HandleJump()
    {
        if (_shouldJump)
        {
            _moveDirection.y = _jumpForce;
        }
    }

    private void HandleHeadBob()
    {
        if (!_characterController.isGrounded) return;

        if (Mathf.Abs(_moveDirection.x) > 0.1f || Mathf.Abs(_moveDirection.z) > 0.1f)
        {
            _timer += Time.deltaTime * (_isSprinting ? _sprintBobSpeed : _walkBobSpeed);
            _playerCamera.transform.localPosition = new Vector3(
                _playerCamera.transform.localPosition.x,
                _defaultYPos + Mathf.Sin(_timer) * (_isSprinting ? _sprintBobAmount : _walkBobAmount),
                _playerCamera.transform.localPosition.z);
        }
    }
    private void HandleFootsteps()
    {
        if (!_characterController.isGrounded) return;
        if (_currentInput == Vector2.zero) return;

        _footstepTimer -= Time.deltaTime;

        if (_footstepTimer <= 0)
        {
            _footstepAudioSource.PlayOneShot(_stepClips[UnityEngine.Random.Range(0, _stepClips.Length - 1)]);
            _footstepTimer = _currentOffset;
        }
    }

    private void HandleInteraction()
    {
        Ray ray = Camera.main.ViewportPointToRay(interactionRayPoint);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, rayDistance))
        {
            Debug.Log(hit.collider.name);
            if(hit.collider.gameObject.GetComponent<IInteractable>() != null)
            {
                if (Input.GetMouseButton(0)) hit.collider.gameObject.GetComponent<IInteractable>().Interact();

                interactionPointer.color = greenColor;
            }
            else
            {
                Debug.Log("Smth");
                interactionPointer.color = whiteColor;
            }
        }
        else
        {
            interactionPointer.color = whiteColor;
        }

        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red, 1f);
    }
    private void SetColor()
    {
        whiteColor = Color.white;
        greenColor = Color.green;

        whiteColor.a = 0.6f;
        greenColor.a = 0.6f;
    }

    void OnDrawGizmos()
    {
        Ray ray = Camera.main.ViewportPointToRay(interactionRayPoint);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray.origin, ray.direction * rayDistance);
    }
}
