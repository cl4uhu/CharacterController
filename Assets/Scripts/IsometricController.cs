using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricController : MonoBehaviour
{
    private CharacterController _controller;
    private float _horizontal;
    private float _vertical;

    [SerializeField] private float playerSpeed = 5;
    private float turnSmoothVelocity;
    [SerializeField] float turnSmoothTime = 0.1f; 

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent <CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        Movement();
    }
    void Movement()
    {
        Vector3 direction = new Vector3(_horizontal, 0, _vertical);

        if(direction != Vector3.zero)
        {
           float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg; //movimiento
           float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime); //movimiento suavizado, rotación actual a la que quiero llegar indicando su tiempo 

            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);

            _controller.Move(direction * playerSpeed * Time.deltaTime); 
        } 

    }
}
