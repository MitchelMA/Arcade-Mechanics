using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float explosionPower = 20f;
    [SerializeField] private float explosionRadius = 10f;
    [SerializeField] private float maxLifetime = 10f;
    [SerializeField] private int bombDamage = 50;

    private float _currentLifetime;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.position += (transform.forward) * speed * Time.deltaTime;
        
        _currentLifetime += Time.deltaTime;
        if (_currentLifetime >= maxLifetime)
            Explode();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    private void Explode()
    {
        var hits = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hit in hits)
        {
            if (hit.gameObject.TryGetComponent(out Rigidbody body))
            {
                body.AddForce((body.transform.position - transform.position).normalized * explosionPower,
                    ForceMode.Impulse);
            }

            if (hit.gameObject.TryGetComponent(out BoxHealth health))
            {
                health.Damage(bombDamage);
                if (!health.IsAlive())
                    // Deactivate object so the box manager can clean it up
                    hit.gameObject.SetActive(false);
            }
        }
        Destroy(gameObject);
    }
}
