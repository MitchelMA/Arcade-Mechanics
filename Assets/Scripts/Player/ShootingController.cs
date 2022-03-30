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

        private float _remainingCooldown;

        // Update is called once per frame
        private void Update()
        {
            _remainingCooldown = Mathf.Max(_remainingCooldown - Time.deltaTime, 0);
            if (Mouse.current.leftButton.wasPressedThisFrame && _remainingCooldown == 0)
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
                
                AudioSource shootAudio = gameObject.AddComponent<AudioSource>();
                shootAudio.clip = shootSound;
                shootAudio.Play(0);

                _remainingCooldown = cooldown;
            }
        }
    }
}