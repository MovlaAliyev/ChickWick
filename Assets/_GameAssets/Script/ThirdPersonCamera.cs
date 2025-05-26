using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{

    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _orientationTransfrom;
    [SerializeField] private Transform _playerVisualsTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewDirection
            = _playerTransform.position - new Vector3(transform.position.x, _playerTransform.position.y, transform.position.z);

        _orientationTransfrom.forward = viewDirection.normalized;

        float _verticalInput = Input.GetAxis("Vertical");
        float _horizontalInput = Input.GetAxis("Horizontal");

        Vector3 moveDirection = _orientationTransfrom.forward * _verticalInput + _orientationTransfrom.right * _horizontalInput;

        if (moveDirection != Vector3.zero)
        {
             _playerVisualsTransform.forward
                = Vector3.Slerp(_playerVisualsTransform.forward, moveDirection.normalized, Time.deltaTime * _rotationSpeed);     
        }
          
    }
}
