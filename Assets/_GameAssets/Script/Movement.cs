using System;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public event Action OnPlayerJumped;
    private bool _isSliding;
    private Vector3 _moveDirection;
    private StateController _playerState;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _slideDrag;
    [SerializeField] private float _groundDrag;
    [SerializeField] private float _slidingSpeed;
    [SerializeField] private float _playerHeight;

    private float _startMovementSpeed, _startJumpForce;
    private float _verticalInput, _horizontalInput;

    [SerializeField] private KeyCode _slidingKey;
    [SerializeField] private KeyCode _movementKey;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private Transform _orientationTransfrom;

    void Awake()
    {
        _playerState = GetComponent<StateController>();
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.freezeRotation = true;

        _startJumpForce = _jumpForce;
        _startMovementSpeed = _speed;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetInputs();
        SetStates();
        PlayerDrag();
        LimitPlayerSeed();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void PlayerDrag()
    {
        _playerRigidbody.linearDamping = _isSliding ? _slideDrag : _groundDrag;
    }

    private void SetInputs()
    {
        _verticalInput = Input.GetAxis("Vertical");
        _horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump();
        }
        else if (Input.GetKeyDown(_slidingKey))
        {
            _isSliding = true;
        }
        else if (Input.GetKeyDown(_movementKey))
        {
            _isSliding = false;
        }
    }

    private void LimitPlayerSeed()
    {
        Vector3 flatVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0, _playerRigidbody.linearVelocity.z);
        if (flatVelocity.magnitude > _speed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * _speed;
            _playerRigidbody.linearVelocity = new Vector3(limitedVelocity.x, _playerRigidbody.linearVelocity.y, limitedVelocity.z);
        }

    }

    private void Move()
    {
        _moveDirection = _orientationTransfrom.forward * _verticalInput + _orientationTransfrom.right * _horizontalInput;
        if (_isSliding)
        {
            _playerRigidbody.AddForce(_moveDirection.normalized * _speed * _slidingSpeed, ForceMode.Force);
        }
        else
        {
            _playerRigidbody.AddForce(_moveDirection.normalized * _speed, ForceMode.Force);
        }
    }

    private void Jump()
    {
        OnPlayerJumped?.Invoke();
        _playerRigidbody.linearVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0, _playerRigidbody.linearVelocity.z);
        _playerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    private bool IsGrounded()
    {
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = Vector3.down;
        float rayLength = _playerHeight * 0.5f + 0.2f;

        bool grounded = Physics.Raycast(rayOrigin, rayDirection, rayLength, _groundMask);
        Debug.DrawRay(rayOrigin, rayDirection * rayLength, grounded ? Color.green : Color.red);

        return grounded;
    }

    private void SetStates()
    {
        var movementDirection = _moveDirection.normalized;
        var isGrounded = IsGrounded();
        var currentState = _playerState.GetPlayerState();

        var newState = currentState switch
        {
            _ when movementDirection == Vector3.zero && isGrounded && !_isSliding => PlayerState.IDLE,
            _ when movementDirection != Vector3.zero && isGrounded && !_isSliding => PlayerState.MOVE,
            _ when movementDirection != Vector3.zero && isGrounded && _isSliding => PlayerState.SLIDE,
            _ when movementDirection == Vector3.zero && isGrounded && _isSliding => PlayerState.SLIDEIDLE,
            _ when !isGrounded => PlayerState.JUMP,
            _ => currentState
        };

        if (newState != currentState)
        {
            _playerState.ChangePlayerState(newState);
        }
    }

    public void SetMovementSpeed(float newSpeed, float duration)
    {
        Debug.Log($"speed: {_speed}");
        Debug.Log($"newSpeed: {newSpeed}");
        _speed += newSpeed;
        Invoke(nameof(ResetMovementSpeed), duration);
    }

    public void ResetMovementSpeed()
    {
        _speed = _startMovementSpeed;
    }

    public void SetJumpForce(float newJumpForce, float duration)
    {
        _jumpForce += newJumpForce;
        Invoke(nameof(ResetJumpForce), duration);
    }

    public void ResetJumpForce()
    {
        _jumpForce = _startJumpForce;
    }

    public Rigidbody GetPlayerRigidbody()
    {
        return _playerRigidbody;
    }
}
