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

        private Animator _animator;
        // Start is called before the first frame update
        void Start()
        {
            _movement = GetComponent<PlayerMovement>();
            _animator = GetComponentInChildren<Animator>();
        }
        

        private void Update()
        {
            var vel = _movement.velocity;
            var gravityDelta = -_movement.gravity * Time.fixedTime;
            // Detect Jumping and Falling
            if (vel.y < -gravityDelta || vel.y > gravityDelta)
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