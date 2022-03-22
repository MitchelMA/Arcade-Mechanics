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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(headPoint.position, headPoint.forward * 100f, Color.cyan);
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Instantiate(projectile, headPoint.position + (headPoint.forward * 2f), headPoint.rotation);
            // if (Physics.Raycast(headPoint.position, headPoint.forward, out RaycastHit hit, 1000f, mask))
            // {
            //     // Instantiate(projectile, hit.point, headPoint.rotation);
            //     // Debug.DrawRay(headPoint.position, headPoint.forward * hit.distance, Color.green);
            //     var spawned = Instantiate(projectile, firePoint.position, firePoint.rotation);
            //     var bp = spawned.GetComponent<BombProjectile>();
            //     bp.SetTargetPoint(hit.point);
            // }
        }
    }
}