using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Vector3 _moveDirection;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _playerHeight;
    private float _verticalInput, _horizontalInput;

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
    }

    void FixedUpdate()
    {
        Move();
    }

    private void SetInputs()
    {
        _verticalInput = Input.GetAxis("Vertical");
        _horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            Jump();
        }
    }

    private void Move()
    {
        _moveDirection = _orientationTransfrom.forward * _verticalInput + _orientationTransfrom.right * _horizontalInput;
        _playerRigidbody.AddForce(_moveDirection.normalized * _speed, ForceMode.Force);
    }

    private void Jump()
    {
        _playerRigidbody.velocity = new Vector3(_playerRigidbody.velocity.x, 0, _playerRigidbody.velocity.z);
        _playerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse); 
    }
    
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundMask);
    }
}
