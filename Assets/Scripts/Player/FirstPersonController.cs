using JetBrains.Annotations;
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

        private CharacterController _charcon;

        private Vector2 _moveInput = Vector2.zero;

        private Vector3 _velocity = Vector3.zero;

        public Vector3 Movement { get; private set; } = Vector3.zero;

        private bool _sprinting;

        public bool Grounded => _grounded;
        private bool _grounded;


        // Start is called before the first frame update
        private void Start()
        {
            _charcon = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
        }
        

        // Update is called once per frame
        private void Update()
        {
            LockUnlockMouse();
            HandleMovement();
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

            var vInp = _moveInput.y;
            var hInp = _moveInput.x;
            
            var objTransform = transform;

            Movement = objTransform.forward * vInp + objTransform.right * hInp;
            Movement = Vector3.ClampMagnitude(Movement, 1);

            // Apply gravity
            _velocity.y += gravityValue * Time.deltaTime;

            Movement *= (_sprinting ? sprintspeed : walkspeed);

            Movement += _velocity;

            _charcon.Move(Movement * Time.deltaTime);
        }

        private void LockUnlockMouse()
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
        

        public void MoveCallback(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();
        }

        public void SprintCallback(InputAction.CallbackContext context)
        {
            _sprinting = !context.canceled;
        }

        #endregion
        
    }
}