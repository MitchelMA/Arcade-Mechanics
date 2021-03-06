using UnityEngine;

namespace Bomb
{
    public class BombProjectile : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private float explosionPower = 20f;
        [SerializeField] private float explosionRadius = 10f;
        [SerializeField] private float maxLifetime = 10f;
        [SerializeField] private int bombDamage = 50;
        [SerializeField] private GameObject explosion;

        private float _currentLifetime;
        
        // Update is called once per frame
        void Update()
        {
            transform.Translate((transform.forward) * speed * Time.deltaTime, Space.World);
        
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
        
            // instantiate Explosion prefab
            Instantiate(explosion, transform.position, new Quaternion(0, 0, 0, 0));
        
            Destroy(gameObject);
        }
    }
}
