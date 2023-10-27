using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    CharacterController _controller; 
    Transform _fpsCamera;

    [SerializeField] float _speed;
    [SerializeField] float _sensitivity = 200; 
    [SerializeField] float _jumpHeigh;

    float _xRotation = 0;

    // Start is called before the first frame update
    void Awake()
    {
       _controller = GetComponent<CharacterController>(); 
       _fpsCamera = Camera.main.transform; 
    }

    void Update()
    {
        Movement();
    }

    // Update is called once per frame
    void Movement()
    {
        float mouseX = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime; 
        float mouseY = Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime; 

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90, 90); 

        _fpsCamera.localRotation = Quaternion.Euler(_xRotation, 0, 0);

        transform.Rotate(Vector3.up *mouseX);

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        _controller.Move(move.normalized * _speed * Time.deltaTime); 
    }
}
