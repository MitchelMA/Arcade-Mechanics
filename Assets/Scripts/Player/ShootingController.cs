using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform headPoint;
    [SerializeField] private LayerMask mask;

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
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
        }
    }
}