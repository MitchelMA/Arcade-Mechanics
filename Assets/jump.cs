using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    public float jumpHeight = 20;

    private bool isGrounded = true;
    
    private bool jumping = false;

    private Rigidbody body;
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && isGrounded)
        {
            jumping = true;
        }
    }

    void FixedUpdate()
    {
        if (jumping)
        {
            jumping = false;
            var newVel = body.velocity;

            newVel += Vector3.up * jumpHeight;

            body.velocity = newVel;
        }
    }

    // TODO: move grounded logic to groundCollider child
    private void OnTriggerEnter(Collider other)
    {
        isGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
    }
}
