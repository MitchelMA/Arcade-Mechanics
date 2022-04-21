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
                _t = Mathf.Min(_t + speed * Time.deltaTime, 1f);
                // if t greater than 1, change direction
                if (_t >= 1f)
                {
                    _dir = false;
                }
            }
            else
            {
                _t = Mathf.Max(_t - speed * Time.deltaTime, 0f);
                // if t less than 1, chnge direction
                if (_t <= 0f)
                {
                    _dir = true;
                }
            }
        }
    }
}