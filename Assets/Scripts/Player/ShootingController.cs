using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class ShootingController : MonoBehaviour
    {
        [SerializeField] private GameObject projectile;
        [SerializeField] private Transform firePoint;
        [SerializeField] private Transform headPoint;
        [SerializeField] private LayerMask mask;
        [SerializeField] private float cooldown = 0.5f;
        [SerializeField] private AudioClip shootSound;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Animator animator;
        
        private float _remainingCooldown;
        private static readonly int Fire = Animator.StringToHash("fire");

        public void SetProjectile(GameObject proj)
        {
            projectile = proj;
        }

        // Update is called once per frame
        private void Update()
        {
            _remainingCooldown = Mathf.Max(_remainingCooldown - Time.deltaTime, 0);
            if (Mouse.current.leftButton.isPressed && _remainingCooldown == 0)
            {
                Vector3 shootLoc;
                if (Physics.Raycast(headPoint.position, headPoint.forward, out RaycastHit hit, 1000f, mask))
                {
                    shootLoc = hit.point;
                }
                else
                {
                    shootLoc = headPoint.position + headPoint.forward * 100f;
                }

                firePoint.LookAt(shootLoc, Vector3.up);
                Instantiate(projectile, firePoint.position, firePoint.rotation);
                
                // shooting sound
                audioSource.PlayOneShot(shootSound);
                _remainingCooldown = cooldown;
                
                // shooting animation
                animator.SetTrigger(Fire);
            }
        }
    }
}