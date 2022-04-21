using System;
using UnityEngine;

namespace Heatseeking
{
    public class SeekingProjectile : MonoBehaviour
    {
        #region Publics

    

        #endregion

        #region Serialized

        [SerializeField] 
        private float speed = 8;
    
        [SerializeField]
        private float maxForce = 0.4f;

        [Range(0, 180)]
        [SerializeField] private float fieldOfView = 45;

        [SerializeField] private int bombDamage = 50;
        [SerializeField] private float explosionRadius = 5f;
        [SerializeField] private float explosionPower = 10f;
        [SerializeField] private GameObject explosion;
    
        [SerializeField] private float maxLifetime = 10f;
        [SerializeField] private float seekAtLifetime = 1f;
        #endregion

        #region Privates
        private Vector3 _velocity;
        private float _currentLifetime = 0f;
        #endregion

        private void OnCollisionEnter(Collision collision)
        {
            Explode();
        }

        // Update is called once per frame
        void Update()
        {

            // Search all GameObjects with a tag "HeatSeekTarget"
            var targets = GameObject.FindGameObjectsWithTag("HeatSeekTarget");
            // find the best target
            var best = FindBest(targets);
            
            if (best != null && _currentLifetime > seekAtLifetime)
                Seek(best.transform.position, fieldOfView);
            else
                transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
            
            if(_currentLifetime > maxLifetime)
                Explode();
            
            _currentLifetime += Time.deltaTime;
        }

        private void Seek(Vector3 target, float FOV)
        {
            // calculate the desired velocity
            Vector3 desired = target - transform.position;
            desired = SetMag(desired, speed * Time.deltaTime);
        
            // the angle between the target and the velocity
            float angle = Vector3.Angle(transform.forward, desired);
            
            Vector3 steering;
        
            // see if the angle between target and velocity is greater or lesser than the FOV
            if (angle > FOV)
                steering = Vector3.zero;
            else
                steering = desired - _velocity;
        
            // limit the steering vector and apply it to the velocity
            steering = Vector3.ClampMagnitude(steering, maxForce * Time.deltaTime);
            ApplyForce(steering);
        }

        private void ApplyForce(Vector3 force)
        {
            // add the force to the velocity
            _velocity += force;
            // limit the velocity by the max speed
            _velocity = Vector3.ClampMagnitude(_velocity, speed * Time.deltaTime);
            // calculate the new direction
            Vector3 newDir = Vector3.RotateTowards(transform.eulerAngles, _velocity, 2 * Mathf.PI, 2*maxForce);
            // rotate according to this new direction
            transform.rotation = Quaternion.LookRotation(newDir);
            
            // translate forwards
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    
        /// <summary>
        /// Method to filter between near targets to find best possible
        /// </summary>
        /// <param name="objects">Array of GameObjects to filter</param>
        /// <returns>The best possible GameObject from the array</returns>
        private GameObject FindBest(GameObject[] objects)
        {
            if (objects.Length == 0)
                return null;
            var currentBest = objects[0];
            var bestAngle = 360f;
            var bestDistance = Mathf.Infinity;
            foreach (var go in objects)
            {
                // calculate the offset vector for the angle calculation
                var offset = go.transform.position - transform.position;
                var angle = Vector3.Angle(transform.forward, offset);
                var dist = offset.magnitude;

                if ((angle < bestAngle && angle < fieldOfView) || dist < bestDistance)
                {
                    // if they are better than the previous, save them to the local variables
                    bestAngle = angle;
                    bestDistance = dist;
                    currentBest = go;
                }
            }
            
            return currentBest;
        }
    
    
        /// <summary>
        /// Method to set the magnitude of a vector
        /// </summary>
        /// <param name="vector">Input vector</param>
        /// <param name="mag">New magnitude</param>
        /// <returns>A vector3 with the new magnitude</returns>
        private Vector3 SetMag(Vector3 vector, float mag)
        {
            if (vector.magnitude == 0) return vector;
            vector.Normalize();
            vector *= mag;
            return vector;
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
