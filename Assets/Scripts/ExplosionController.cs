using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ExplosionController : MonoBehaviour
{
    [SerializeField] private float duration = 1;

    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponentInChildren<AudioSource>();
        source.Play(0);
        Destroy(gameObject, duration);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
