using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class AnimationSound : MonoBehaviour
    {
        [SerializeField]
        private AudioSource playerSource;

        [SerializeField]
        private AudioClip flap;

        [SerializeField] private AudioClip footstep;
        public void Flap()
        {
            playerSource.PlayOneShot(flap);
        }

        public void Step()
        {
            playerSource.PlayOneShot(footstep);
        }
    }
}
