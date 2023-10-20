using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SoulderController : MonoBehaviour
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

    [SerializeField] private AxisState xAxis;
    [SerializeField] private AxisState yAxis;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
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
