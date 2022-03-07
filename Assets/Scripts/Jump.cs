using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float JumpHeight = 5;
    public float JumpCooldown = 0.4f;
    public int MaxJumps = 2;

    public int JumpParticleAmount = 10;
    
    private CharacterController _body;
    
    private Vector3 _velocity;
    private float gravityValue = -9.81f;
    private bool _isGrounded = true;

    private float _currentJumpCooldown = 0;
    private int _jumpsRemaining;
    private Animator _animator;

    private ParticleSystem _particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<CharacterController>();
        // _particleSystem = GetComponent<ParticleSystem>();
        _jumpsRemaining = MaxJumps;
        // _animator = GetComponent<Animator>();
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
        if (Input.GetButtonDown("Jump") && _jumpsRemaining > 0 && _currentJumpCooldown == 0)
        {
            _jumpsRemaining--;
            _currentJumpCooldown = JumpCooldown;

            if (_jumpsRemaining < MaxJumps - 1)
            {
                _velocity.y = 0f;
                // _particleSystem.Emit(JumpParticleAmount);
            }
            // _animator.SetBool("Jumping", true);

            // https://en.wikipedia.org/wiki/Acceleration#Uniform_acceleration
            _velocity.y += Mathf.Sqrt(JumpHeight * -2f * gravityValue);
        }

        // Apply gravity
        _velocity.y += gravityValue * Time.deltaTime;
        _body.Move(_velocity * Time.deltaTime);
    }

    void FixedUpdate()
    {
        // Putting this in Update() becomes unpredictable and weird
        _isGrounded = _body.isGrounded;

        Debug.LogFormat("IsGrounded: {0}", _isGrounded);


        // Reset jumping variable
        if (_isGrounded)
        {
            _jumpsRemaining = MaxJumps;
            // _animator.SetBool("Jumping", false);
        }

        _currentJumpCooldown = Math.Max(_currentJumpCooldown - Time.fixedDeltaTime, 0);
    }
}
