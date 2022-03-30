using UnityEngine;

namespace Bomb
{
    public class ExplosionController : MonoBehaviour
    {
        [SerializeField] private AudioClip clip;
    
        // Start is called before the first frame update
        void Start()
        {
            var source = GetComponentInChildren<AudioSource>();
            source.PlayOneShot(clip);
            Destroy(gameObject, clip.length);
        }
    }
}
