using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class LookController : MonoBehaviour
    {
        private bool _hasFocus;
        [SerializeField] private float mouseSens = 0.075f;
        [SerializeField] private Transform headTransform;

        private Vector2 _lookInput = Vector2.zero;

        // Update is called once per frame
        void Update()
        {
            var val = _lookInput;

            var xInp = val.x * mouseSens;
            var yInp = val.y * mouseSens;
        
            float ClampAngle(float angle, float min, float max)
            {
                if (angle < 0f) angle = 360 + angle;
                if (angle > 180f) return Mathf.Max(angle, 360 + min);
                return Mathf.Min(angle, max);
            }

            // Only do mouse things when locked.
            if (Cursor.lockState == CursorLockMode.Locked && _hasFocus)
            {
                transform.Rotate(Vector3.up * xInp);

                var rot = headTransform.eulerAngles;
                rot.x -= yInp;
                rot.x = ClampAngle(rot.x, -90, 90);
                headTransform.eulerAngles = rot;
            }
        }
    
        public void LookCallback(InputAction.CallbackContext context)
        {
            _lookInput = context.ReadValue<Vector2>();
        }
    
        private void OnApplicationFocus(bool hasFocus)
        {
            _hasFocus = hasFocus;
        }
    }
}
