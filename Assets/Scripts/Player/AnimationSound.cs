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
        void Flap()
        {
            playerSource.PlayOneShot(flap);
        }

        void Step()
        {
            playerSource.PlayOneShot(footstep);
        }
    }
}
