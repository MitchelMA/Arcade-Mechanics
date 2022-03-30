using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class BoxSpawner : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform parent;
        [SerializeField] private GameObject boxType;

        private readonly List<GameObject> _boxes = new List<GameObject>();
        
        public void Interact()
        {
            for (var x = 0; x < 10; x+=2)
            {
                for (var z = 0; z < 10; z+=2)
                {
                    for (var y = 0; y < 10; y+=2)
                    {
                        _boxes.Add(Instantiate(boxType, parent.position + (new Vector3(x, y, z)), parent.rotation,
                            parent));
                    }
                }
            }
        }

        private void Start()
        {
            InvokeRepeating(nameof(Cleanup), 2f, 2f);
        }

        private void Cleanup()
        {
            var markedForDeath = _boxes.Where(box => box.transform.position.y < -10f).ToList();
            
            foreach (var box in markedForDeath)
            {
                _boxes.Remove(box);
                Destroy(box.gameObject);
            }
        }

        public string GetHoverText()
        {
            return "Spawn Boxes";
        }
    }
}