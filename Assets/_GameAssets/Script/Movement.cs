using UnityEngine;

public class Movement : MonoBehaviour
{
    private Vector3 _moveDirection;

    private bool _isSliding;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _slideDrag;
    [SerializeField] private float _groundDrag;
    [SerializeField] private float _slidingSpeed;
    [SerializeField] private float _playerHeight;
    private float _verticalInput, _horizontalInput;

    [SerializeField] private KeyCode _slidingKey;
    [SerializeField] private KeyCode _movementKey;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private Transform _orientationTransfrom;

    void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.freezeRotation = true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetInputs();
        PlayerDrag();
        LimitPlayerSeed();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void PlayerDrag()
    {
        _playerRigidbody.drag = _isSliding ? _slideDrag : _groundDrag;
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
            Debug.Log("Sliding");
        }
        else if (Input.GetKeyDown(_movementKey))
        {
            _isSliding = false;
            Debug.Log("NotSliding");
        }
    }

    private void LimitPlayerSeed()
    {
        Vector3 flatVelocity = new Vector3(_playerRigidbody.velocity.x, 0, _playerRigidbody.velocity.z);
        if (flatVelocity.magnitude > _speed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * _speed;
            _playerRigidbody.velocity = new Vector3(limitedVelocity.x, _playerRigidbody.velocity.y, limitedVelocity.z);
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
        _playerRigidbody.velocity = new Vector3(_playerRigidbody.velocity.x, 0, _playerRigidbody.velocity.z);
        _playerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse); 
    }
    
    private bool IsGrounded()
    {
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = Vector3.down;
        float rayLength = _playerHeight * 0.4f + 0.1f;

        bool grounded = Physics.Raycast(rayOrigin, rayDirection, rayLength, _groundMask);
        Debug.DrawRay(rayOrigin, rayDirection * rayLength, grounded ? Color.green : Color.red);

        return grounded;
    }
}
