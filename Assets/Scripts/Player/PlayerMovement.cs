using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public float JumpHeight = 1;
        public float JumpCooldown = 0.4f;
        public int MaxJumps = 2;

        public int JumpParticleAmount = 10;
    
        private CharacterController _body;


        [SerializeField]
        private float _moveSpeed = 10f;
        private float _rotationSpeed = 200f;
        
        private Vector3 _velocity;
        public Vector3 velocity => _velocity;
        
        [SerializeField]
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
            _particleSystem = GetComponentInChildren<ParticleSystem>();
            _jumpsRemaining = MaxJumps;
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
                    _particleSystem.Emit(JumpParticleAmount);
                }

                // https://en.wikipedia.org/wiki/Acceleration#Uniform_acceleration
                _velocity.y += Mathf.Sqrt(JumpHeight * -2f * gravityValue);
            }

            // Apply gravity
            _velocity.y += gravityValue * Time.deltaTime;
            var vertInput = Input.GetAxis("Vertical");
            var rotation = Input.GetAxisRaw("Horizontal");
            
            rotation = rotation * _rotationSpeed * Time.deltaTime;
            
            var forward = transform.forward;
            forward  *= (vertInput * _moveSpeed);
            forward.y = velocity.y;
            _velocity = forward;
            _body.Move(_velocity * Time.deltaTime);
            transform.Rotate(0, rotation, 0);
        }

        void FixedUpdate()
        {
            // Putting this in Update() becomes unpredictable and weird
            _isGrounded = _body.isGrounded;

            // Reset jumping variable
            if (_isGrounded)
            {
                _jumpsRemaining = MaxJumps;
            }

            _currentJumpCooldown = Math.Max(_currentJumpCooldown - Time.fixedDeltaTime, 0);
        }
    }
}