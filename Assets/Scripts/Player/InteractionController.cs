using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class InteractionController : MonoBehaviour
    {
        [SerializeField] private Transform headTransform;

        [SerializeField] private LayerMask interactMask;
        [SerializeField] private float interactDistance = 50f;

        [CanBeNull] private IInteractable _targetedObj;

        [SerializeField] [CanBeNull] private TextMeshProUGUI hoverText;

        // Update is called once per frame
        private void Update()
        {
            RayCastInteractables();
            
            // TODO: move to input system
            if (Keyboard.current.eKey.wasPressedThisFrame && _targetedObj != null)
            {
                _targetedObj.Interact();
            }
        }
    
        private void RayCastInteractables()
        {
            if (Physics.Raycast(headTransform.position, headTransform.forward.normalized, out var hit, interactDistance,
                    interactMask))
            {
                Debug.DrawRay(headTransform.position, headTransform.forward.normalized * hit.distance,
                    Color.yellow);

                if (hit.transform.gameObject.CompareTag("Interactable"))
                {
                    _targetedObj = hit.transform.gameObject.GetComponent<IInteractable>();
                    if (_targetedObj != null) UpdateText($"[E] {_targetedObj.GetHoverText()}");
                }
                else
                {
                    _targetedObj = null;
                    UpdateText("");
                }
            }
            else
            {
                _targetedObj = null;
                UpdateText("");
            }
        }
    
        private void UpdateText(string inp)
        {
            if (hoverText != null)
            {
                hoverText.text = inp;
            }
        }
    }
}
