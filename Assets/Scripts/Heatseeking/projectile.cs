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
    #endregion

    #region Privates
    [SerializeField]
    private Vector3 velocity = Vector3.zero;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        seek(seekTarget.position);
    }

    private void seek(Vector3 target)
    {
        Vector3 desired = target - transform.position;
        desired = SetMag(desired, speed * Time.deltaTime);
        Vector3 steering = desired - velocity;
        steering = Limit(steering, maxForce * Time.deltaTime);
        ApplyForce(steering);
    }

    private void ApplyForce(Vector3 force)
    {
        velocity += force;
        velocity = Limit(velocity, speed * Time.deltaTime);
        Vector3 newDir = Vector3.RotateTowards(transform.eulerAngles, velocity, 2 * Mathf.PI, 2*maxForce);
        transform.rotation = Quaternion.LookRotation(newDir);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    
    
    
    // Vector Methods
    private Vector3 SetMag(Vector3 vector, float mag)
    {
        if (vector.magnitude == 0) return vector;
        vector.Normalize();
        vector *= mag;
        return vector;
    }

    private Vector3 Limit(Vector3 vector, float limit)
    {
        if (vector.magnitude == 0 || vector.magnitude < limit) return vector;
        vector.Normalize();
        vector *= limit;
        return vector;
    }
}
