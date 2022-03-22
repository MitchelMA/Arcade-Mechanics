using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class BoxSpawner : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform parent;
        [SerializeField] private GameObject boxType;

        private List<GameObject> boxes = new List<GameObject>();
        public void Interact()
        {
            boxes.Add(Instantiate(boxType, parent));
        }

        private void FixedUpdate()
        {
            List<GameObject> markedForDeath = new List<GameObject>();
            foreach (var box in boxes)
            {
                if (box.transform.position.y < -10f)
                {
                    markedForDeath.Add(box);
                }
            }

            foreach (var box in markedForDeath)
            {
                boxes.Remove(box);
                Destroy(box.gameObject);
            }
        }

        public string GetHoverText()
        {
            return "Spawn Boxes";
        }
    }
}