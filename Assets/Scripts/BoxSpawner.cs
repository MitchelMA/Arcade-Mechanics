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
            for (int x = 0; x < 5; x++)
            {
                for (int z = 0; z < 5; z++)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        boxes.Add(Instantiate(boxType, parent.position + (new Vector3(x, y, z)), parent.rotation,
                            parent));
                    }
                }
            }
        }

        private void Start()
        {
            InvokeRepeating("Cleanup", 2f, 2f);
        }

        private void Cleanup()
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