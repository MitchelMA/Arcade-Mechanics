using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        private PlayerMovement _movement;

        private float _groundDistance = 0.125f;

        private Animator _animator;
        // Start is called before the first frame update
        void Start()
        {
            _movement = GetComponent<PlayerMovement>();
            _animator = GetComponentInChildren<Animator>();
        }
        

        public void FixedUpdate()
        {
            var vel = _movement.velocity;
            bool isGrounded = false;
            // Use raycast bc charactercontroller is funky
            // TODO: Add layermask for terrain only
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit))
            {
                var distance = hit.distance;
                isGrounded = distance < _groundDistance;
                
                var position = transform.position;
                Debug.DrawLine (position, position + Vector3.down * _groundDistance, Color.cyan);
            }
            // Detect Jumping and Falling
            if (!isGrounded)
            {
                _animator.SetBool("Falling", true);
            }
            else
            {
                _animator.SetBool("Falling", false);
            }
            // Detect walking
            // TODO: FIX IS BROKEN
            var move = vel.x + vel.z;
            if (move != 0)
            {
                _animator.SetBool("Walking", true);
            }
            else
            {
                _animator.SetBool("Walking", false);
            }
        }
    }
}