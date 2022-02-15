using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    public float jumpHeight = 5;
    
    private CharacterController _body;
    
    private Vector3 _velocity;
    private float gravityValue = -9.81f;
    private bool _isGrounded = true;

    private bool _jumping;
    
    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<CharacterController>();
    }

    // Update is called once per frame

    void Update()
    {
        // Reset velocity y if on ground;
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0f;
        }
        
        // If jumping increase y velocity
        if (Input.GetButton("Jump") && _isGrounded && !_jumping)
        {
            Debug.Log("JUMP");
            _jumping = true;
            _velocity.y += jumpHeight - gravityValue;
        }

        // Apply gravity
        _velocity.y += gravityValue * Time.deltaTime;
        _body.Move(_velocity * Time.deltaTime);
    }

    void FixedUpdate()
    {
        // Putting this in Update() becomes unpredictable and weird
        _isGrounded = _body.isGrounded;
        
        // Reset jumping variable
        if (_isGrounded) _jumping = false;
    }
}
