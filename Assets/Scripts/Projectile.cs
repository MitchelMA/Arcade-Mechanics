using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region public vars

    #endregion

    #region serialized fields

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float maxSpeed = 2;

    [SerializeField]
    private float maxForce = 2;

    [SerializeField]
    private float landingDistance = 5;

    [SerializeField, Range(0, 180)]
    private float fieldOfView = 45;

    [SerializeField]
    private float viewDistance = 100;

    [SerializeField]
    // this is the position the projectile will go to when it loses the player
    private Vector3 residencePosition;

    #endregion

    #region private vars

    private const float MinVelocity = 1f;

    private Vector3 _velocity = new Vector3(0, 0, 0);
    private Vector3 _acceleration = new Vector3(0, 0, 0);
    private Vector3 _direction;

    private Vector3 _currentTarget;

    private const float WaitTime = 4;
    private float _currentWait;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        // add a start velocity to the projectile, otherwise the scalar-projection won't work
        _velocity.x += MinVelocity;
        _velocity.y += MinVelocity;
        _velocity.z += MinVelocity;

        _currentTarget = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        // check if the target is in the field of view AND if the distance is less than the view-distance
        if (Vector3.Angle(_velocity, target.position - transform.position) < fieldOfView && Distance(transform.position, target.position) < viewDistance)
        {
            _currentTarget = target.position;
            _currentWait = WaitTime;
        }
        // if not, go to the place of residence
        else
        {
            _currentTarget = transform.position;
            _currentWait -= Time.deltaTime;
        }

        if(_currentWait <= 0)
        {
            _currentTarget = residencePosition;
        }

        // if the distance to the target is greater than the landingDistance, go to the target;
        // this to not have it continiously try to get to the target
        if (Distance(transform.position, _currentTarget) > landingDistance)
            Seek(_currentTarget);
        // if it's in the vicinity of the current target, slow down 
        else
        {
            if(_velocity.magnitude > MinVelocity)
                _velocity *= 0.98f;
        }

        // update the position
        PosUpdate();
    }

    /// <summary>
    /// Method to seek a target
    /// </summary>
    /// <param name="_target">target you want to go to</param>
    private void Seek(Vector3 _target)
    {
        // create a y-offset from the target, so the projectile doesn't go to the feet of the player
        _target.y += 1;
        // get the desired velocity
        // this is calculated by subtracting the current positions from the target's position
        Vector3 desired = _target - transform.position;
        // set the magnitude of this desired velocity to be the max speed
        desired = SetMag(desired, maxSpeed);

        // you get the steering force by subtracting the current velocity from the desired velocity
        Vector3 steering = desired - _velocity;
        // limit this steering force
        steering = Limit(steering, maxForce);

        // apply the steering force to the acceleration
        ApplyForce(steering);
    }

    /// <summary>
    /// Applies a force to the acceleration
    /// </summary>
    /// <param name="force">force you want to apply</param>
    private void ApplyForce(Vector3 force)
    {
        _acceleration += force;
        _acceleration *= Time.deltaTime;
    }

    /// <summary>
    /// Method to update the position
    /// </summary>
    private void PosUpdate()
    {
        _velocity += _acceleration;
        _velocity = Limit(_velocity, maxSpeed);
    
        transform.Translate(_velocity * Time.deltaTime);
        _acceleration = Vector3.zero;
    }

    /// <summary>
    /// Method to limit the magnitude of a vector
    /// </summary>
    /// <param name="vector">vector you want to limit</param>
    /// <param name="max">max magnitude of the vector</param>
    /// <returns>the limited vector</returns>
    private static Vector3 Limit(Vector3 vector, float max)
    {
        if (vector.magnitude == 0 || vector.magnitude < max) return vector;
        vector.Normalize();
        vector *= max;
        return vector;
    }

    /// <summary>
    /// This method sets the magnitude of a vector
    /// </summary>
    /// <param name="vector">input vector</param>
    /// <param name="magnitude">you want to set</param>
    /// <returns>the vector with the desired magnitude</returns>
    private static Vector3 SetMag(Vector3 vector, float magnitude)
    {
        if (vector.magnitude == 0) return vector;
        vector.Normalize();
        vector *= magnitude;
        return vector;
    }

    /// <summary>
    /// Calculates the difference between two vectors
    /// </summary>
    /// <param name="vectorA">The first vector</param>
    /// <param name="vectorB">The second vector</param>
    /// <returns>Distance between the two vectors</returns>
    private static float Distance(Vector3 vectorA, Vector3 vectorB)
    {
        return (vectorA - vectorB).magnitude;
    }
}
