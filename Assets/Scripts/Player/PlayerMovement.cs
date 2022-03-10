using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        #region public vars
        public Vector3 Velocity => _velocity;
        #endregion
        
        #region Serialized Fields
        [SerializeField]
        private float jumpHeight = 1;
        
        [SerializeField]
        private float jumpCooldown = 0.4f;
        
        [SerializeField]
        private int maxJumps = 2;

        [SerializeField]
        private int jumpParticleAmount = 10;

        [SerializeField]
        private float moveSpeed = 10f;

        [SerializeField]
        private float gravityValue = -9.81f;
        
        [SerializeField]
        private float rotationSpeed = 200f;
        #endregion

        #region private vars
        private Vector3 _velocity;
        private bool _isGrounded = true;
        private float _currentJumpCooldown;
        private int _jumpsRemaining;
        
        private CharacterController _body;
        private ParticleSystem _particleSystem;
        #endregion

        // Start is called before the first frame update
        private void Start()
        {
            _body = GetComponent<CharacterController>();
            _particleSystem = GetComponentInChildren<ParticleSystem>();
            _jumpsRemaining = maxJumps;
        }

        // Update is called once per frame
        private void Update()
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
                _currentJumpCooldown = jumpCooldown;

                if (_jumpsRemaining < maxJumps - 1)
                {
                    _velocity.y = 0f;
                    _particleSystem.Emit(jumpParticleAmount);
                }

                // https://en.wikipedia.org/wiki/Acceleration#Uniform_acceleration
                _velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravityValue);
            }

            // Apply gravity
            _velocity.y += gravityValue * Time.deltaTime;
            var vertInput = Input.GetAxis("Vertical");
            var rotation = Input.GetAxisRaw("Horizontal");
            
            rotation = rotation * rotationSpeed * Time.deltaTime;
            
            var forward = transform.forward;
            forward  *= (vertInput * moveSpeed);
            forward.y = Velocity.y;
            _velocity = forward;
            _body.Move(_velocity * Time.deltaTime);
            transform.Rotate(0, rotation, 0);
        }

        private void FixedUpdate()
        {
            // Putting this in Update() becomes unpredictable and weird
            _isGrounded = _body.isGrounded;

            // Reset jumping variable
            if (_isGrounded)
            {
                _jumpsRemaining = maxJumps;
            }

            _currentJumpCooldown = Math.Max(_currentJumpCooldown - Time.fixedDeltaTime, 0);
        }
    }
}