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

        [CanBeNull] private IInteractable targetedObj;

        [SerializeField] [CanBeNull] private TextMeshProUGUI hoverText;

        // Update is called once per frame
        void Update()
        {
            RayCastInteractables();
            
            // TODO: move to input system
            if (Keyboard.current.eKey.wasPressedThisFrame && targetedObj != null)
            {
                targetedObj.Interact();
            }
        }
    
        private void RayCastInteractables()
        {
            RaycastHit hit;
            if (Physics.Raycast(headTransform.position, headTransform.forward.normalized, out hit, interactDistance,
                    interactMask))
            {
                Debug.DrawRay(headTransform.position, headTransform.forward.normalized * hit.distance,
                    Color.yellow);

                if (hit.transform.gameObject.CompareTag("Interactable"))
                {
                    targetedObj = hit.transform.gameObject.GetComponent<IInteractable>();
                    updateText($"[E] {targetedObj.GetHoverText()}");
                }
                else
                {
                    targetedObj = null;
                    updateText("");
                }
            }
            else
            {
                targetedObj = null;
                updateText("");
            }
        }
    
        private void updateText(string inp)
        {
            if (hoverText != null)
            {
                hoverText.text = inp;
            }
        }
    }
}
