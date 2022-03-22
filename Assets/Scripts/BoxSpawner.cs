using UnityEngine;

namespace DefaultNamespace
{
    public class BoxSpawner : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform parent;
        [SerializeField] private GameObject boxType;
        public void Interact()
        {
            Instantiate(boxType, parent);
        }

        public string GetHoverText()
        {
            return "Spawn Boxes";
        }
    }
}