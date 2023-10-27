using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPS_controller : MonoBehaviour
{
    private CharacterController _controller;
    private float _horizontal;
    private float _vertical;
    private Transform _camera;

    //variables para velocidad, salto y gravedad
    [SerializeField] private float playerSpeed = 5;
    [SerializeField] private float _jumpHeight = 1; 
    
    private float _gravity = -9.81f; 
    private Vector3 _playerGravity; 
   
    //variables para rotacion
    private float turnSmoothVelocity;
    [SerializeField] float turnSmoothTime = 0.1f; 

    //variable para sensor
    [SerializeField] private Transform _sensorPosition;
    [SerializeField] private float _sensorRadius = 0.3f;
    [SerializeField] private LayerMask _groundLayer;
    private bool _isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent <CharacterController>();
        _camera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        if(Input.GetButton("Fire2"))
        {
            AimMovement();
        }
        else
        {
            Movement();
        } 

        Jump();
    }
    void Movement()
    {
        Vector3 direction = new Vector3(_horizontal, 0, _vertical);

        if(direction != Vector3.zero)
        {
           float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y; //movimiento
           float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime); //movimiento suavizado, rotación actual a la que quiero llegar indicando su tiempo 

            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);

            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            _controller.Move(moveDirection.normalized * playerSpeed * Time.deltaTime); 
        } 
    }

    void AimMovement()
    {
        Vector3 direction = new Vector3(_horizontal, 0, _vertical);
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y; //movimiento
        float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _camera.eulerAngles.y, ref turnSmoothVelocity, turnSmoothTime); //movimiento suavizado, rotación actual a la que quiero llegar indicando su tiempo 

        transform.rotation = Quaternion.Euler(0, smoothAngle, 0);

        if(direction != Vector3.zero)
        {
            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            _controller.Move(moveDirection.normalized * playerSpeed * Time.deltaTime); 
        } 
    }
    
    void Jump()
    {
        _isGrounded = Physics.CheckSphere(_sensorPosition.position, _sensorRadius, _groundLayer); 

        if(_isGrounded && _playerGravity.y < 0)
        {
            _playerGravity.y = 0;
        }
        
        if(_isGrounded && Input.GetButtonDown("Jump"))
        {
            //_playerGravity.y = -_jumpHeight; 
            _playerGravity.y = Mathf.Sqrt(_jumpHeight * -2 * _gravity); 
        }

        _playerGravity.y += _gravity * Time.deltaTime;

        _controller.Move(_playerGravity * Time.deltaTime);
    }
}
