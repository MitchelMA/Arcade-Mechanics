using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class AnimationSound : MonoBehaviour
    {
        [SerializeField]
        private AudioSource flapSound;
        public void Flap()
        {
            flapSound.Play();
        }
    }
}
