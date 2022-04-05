using UnityEngine;

namespace Player
{
    public interface IInteractable
    {
        void Interact(GameObject player);

        string GetHoverText();
    }
}