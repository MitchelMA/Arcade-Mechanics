using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class projectile : MonoBehaviour
{
    #region Publics

    

    #endregion

    #region Serialized

    [SerializeField] 
    private float speed = 8;
    
    [SerializeField]
    private float maxForce = 0.4f;

    [SerializeField] 
    private Transform seekTarget;

    [SerializeField] private float fieldOfView = 45;
    
    //MUST: the code must have a non-zero velocity-vector for it to work, so it must have a starting-velocity;
    [SerializeField]
    private Vector3 velocity;
    #endregion

    #region Privates
    #endregion

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // seek the target
        seek(seekTarget.position, fieldOfView);
    }

    private void seek(Vector3 target, float FOV)
    {
        // calculate the desired velocity
        Vector3 desired = target - transform.position;
        desired = SetMag(desired, speed * Time.deltaTime);
        
        // the angle between the target and the velocity
        float angle = Vector3.Angle(velocity, desired);
        
        // steering vector
        Vector3 steering;
        
        // see if the angle between target and velocity is greater or lesser than the FOV
        if (angle > FOV)
            steering = Vector3.zero;
        else
            steering = desired - velocity;
        
        // limit the steering vector and apply it to the velocity
        steering = Limit(steering, maxForce * Time.deltaTime);
        ApplyForce(steering);
    }

    private void ApplyForce(Vector3 force)
    {
        // add the force to the velocity
        velocity += force;
        // limit the velocity bij the max speed
        velocity = Limit(velocity, speed * Time.deltaTime);
        // calculate the new direction
        Vector3 newDir = Vector3.RotateTowards(transform.eulerAngles, velocity, 2 * Mathf.PI, 2*maxForce);
        // rotate according to this new direction
        transform.rotation = Quaternion.LookRotation(newDir);
        // translate forwards
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    
    
    
    /// <summary>
    /// Method to set the magnitude of a vector
    /// </summary>
    /// <param name="vector">Input vector</param>
    /// <param name="mag">New magnitude</param>
    /// <returns>A vector3 with the new magnitude</returns>
    private Vector3 SetMag(Vector3 vector, float mag)
    {
        // 
        if (vector.magnitude == 0) return vector;
        vector.Normalize();
        vector *= mag;
        return vector;
    }
    /// <summary>
    /// Method to limit a vector with a certain magnitude
    /// </summary>
    /// <param name="vector">Input vector</param>
    /// <param name="limit">Maximum magnitude</param>
    /// <returns>A vector of which the magnitude cannot be greater than the specified limit</returns>
    private Vector3 Limit(Vector3 vector, float limit)
    {
        if (vector.magnitude == 0 || vector.magnitude < limit) return vector;
        vector.Normalize();
        vector *= limit;
        return vector;
    }
}
