using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : MonoBehaviour
{
    public float Speed = 10f;
    public float Explosionpower = 20f;
    public float ExplosionRadius = 10f;

    public float MaxLifetime = 10f;

    private float lifetime = 0f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.position += (transform.forward) * Speed * Time.deltaTime;
        
        lifetime += Time.deltaTime;
        if (lifetime >= MaxLifetime)
            Destroy(gameObject);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        var hits = Physics.OverlapSphere(transform.position, ExplosionRadius);
        foreach (var hit in hits)
        {
            if(hit.gameObject.TryGetComponent(out Rigidbody body))
            {
                body.AddForce((body.transform.position - transform.position).normalized * Explosionpower, ForceMode.Impulse);
            }
        }
        Destroy(gameObject);
    }
}
