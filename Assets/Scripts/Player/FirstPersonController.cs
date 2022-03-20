using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField] private float gravityValue = -9.81f;
        [SerializeField] private float walkspeed = 4;
        [SerializeField] private float sprintspeed = 8;
        [SerializeField] private float jumpHeight = 1;

        [SerializeField] private float mouseSens = 0.075f;
        [SerializeField] private Transform headTransform;
        [SerializeField] private Transform cameraTransform;

        [SerializeField] private LayerMask interactMask;
        [SerializeField] private float interactDistance = 50f;

        private CharacterController _charcon;
        private Vector2 _moveInput = Vector2.zero;
        private Vector2 _lookInput = Vector2.zero;

        private Vector3 _velocity = Vector3.zero;
        private PickupItemScriptableObject targetedObj;

        [SerializeField] private TextMeshProUGUI hoverText;

        private bool _sprinting;
        private bool _grounded;

        private bool _hasFocus;

        // Start is called before the first frame update
        void Start()
        {
            _charcon = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            _hasFocus = hasFocus;
        }

        // Update is called once per frame
        void Update()
        {
            HandleLook();
            LockUnlockMouse();
            HandleMovement();
            HandleInteraction();
        }

        void HandleLook()
        {
            var val = _lookInput;

            var xInp = val.x * mouseSens;
            var yInp = val.y * mouseSens;

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

        private void HandleInteraction()
        {
            RaycastHit hit;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward.normalized, out hit, interactDistance,
                    interactMask))
            {
                Debug.DrawRay(cameraTransform.position, cameraTransform.forward.normalized * hit.distance,
                    Color.yellow);

                if (hit.transform.gameObject.CompareTag("Interactable"))
                {
                    targetedObj = hit.transform.gameObject.GetComponent<PickupObject>().Obj;
                    hoverText.text = targetedObj.ItemName;
                }
                else
                {
                    targetedObj = null;
                    hoverText.text = "";
                }
            }
            else
            {
                targetedObj = null;
                hoverText.text = "";
            }
        }

        private void FixedUpdate()
        {
            _grounded = _charcon.isGrounded;
        }

        private void HandleMovement()
        {
            if (_grounded && _velocity.y < 0)
            {
                _velocity.y = 0;
            }

            // if (Input.GetButtonDown(""))
            var vInp = _moveInput.y;
            var hInp = _moveInput.x;
            //
            var objt = transform;

            var direction = objt.forward * vInp + objt.right * hInp;
            direction = Vector3.ClampMagnitude(direction, 1);

            // Apply gravity
            _velocity.y += gravityValue * Time.deltaTime;

            direction *= (_sprinting ? sprintspeed : walkspeed);

            direction += _velocity;

            _charcon.Move(direction * Time.deltaTime);
        }

        void LockUnlockMouse()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }

        #region Input Callbacks

        public void JumpCallback(InputAction.CallbackContext context)
        {
            if (!context.canceled && context.started && _grounded)
            {
                _velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravityValue);
            }
        }

        public void LookCallback(InputAction.CallbackContext context)
        {
            _lookInput = context.ReadValue<Vector2>();
        }

        public void MoveCallback(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();
        }

        public void SprintCallback(InputAction.CallbackContext context)
        {
            _sprinting = !context.canceled;
        }

        #endregion

        float ClampAngle(float angle, float min, float max)
        {
            if (angle < 0f) angle = 360 + angle;
            if (angle > 180f) return Mathf.Max(angle, 360 + min);
            return Mathf.Min(angle, max);
        }
    }
}