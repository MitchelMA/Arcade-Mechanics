using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Target
{
    public class TargetScript : MonoBehaviour
    {
        #region Serialized

        [SerializeField] private Transform startPoint;

        [SerializeField] private Transform endPoint;
        [SerializeField] private float speed = 1f;

        #endregion

        #region Privates

        private float _t = 0;
        private bool _dir = true;

        #endregion

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.position = Vector3.Lerp(startPoint.position, endPoint.position, _t);
            // check for direction
            if (_dir)
            {
                _t += speed * Time.deltaTime;
                // if t greater than 1, change direction
                if (_t > 1)
                {
                    _dir = false;
                }
            }
            else
            {
                _t -= speed * Time.deltaTime;
                // if t less than 1, chnge direction
                if (_t < 0)
                {
                    _dir = true;
                }
            }
        
            // set boundaries for t
            if (_t > 1)
                _t = 1;
            if (_t < 0)
                _t = 0;
        }
    }
}