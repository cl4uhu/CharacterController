using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricController : MonoBehaviour
{
    private CharacterControloler _controller;
    private float _horizxontal;
    private float _vertical;

    [SerializeField] private float playerSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent <CharacterControloler>();
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

        _controller.Move(direction * playerSpeed * Time.deltaTime);
    }
}
