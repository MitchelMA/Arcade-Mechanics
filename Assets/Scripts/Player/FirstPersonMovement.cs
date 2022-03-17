using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class FirstPersonMovement : MonoBehaviour
{
    [SerializeField] private float walkspeed = 4;
    [SerializeField] private float sprintspeed = 8;
    [SerializeField] private Camera camera;
    [SerializeField] private float mouseSens = 0.075f;

    private CharacterController _charcon;
    private Vector2 _moveInput;

    private bool _sprinting;

    // Start is called before the first frame update
    void Start()
    {
        _charcon = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        LockUnlockMouse();
        HandleMovement();
    }

    public void Look(InputAction.CallbackContext context)
    {
        var val = context.ReadValue<Vector2>();

        var xInp = val.x * mouseSens;
        var yInp = val.y * mouseSens;

        // Only do mouse things when locked.
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            transform.Rotate(Vector3.up * xInp);

            var cameraTransform = camera.transform;
            var rot = cameraTransform.eulerAngles;
            rot.x -= yInp;
            rot.x = ClampAngle(rot.x, -90, 90);
            camera.transform.eulerAngles = rot;
        }
    }

    public void Movement(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void ToggleSprint(InputAction.CallbackContext context)
    {
        _sprinting = !context.canceled;
    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + min);
        return Mathf.Min(angle, max);
    }

    private void HandleMovement()
    {
        // if (Input.GetButtonDown(""))
        var vInp = _moveInput.y;
        var hInp = _moveInput.x;
        //
        var obj = transform;
        
        var direction = obj.forward * vInp + obj.right * hInp;
        direction = Vector3.ClampMagnitude(direction, 1);
        
        _charcon.Move(direction * ( _sprinting ? sprintspeed : walkspeed) * Time.deltaTime);
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
}
