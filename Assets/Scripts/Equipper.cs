using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class Equipper : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject projectilePrefab;
    
    public void Interact(GameObject player)
    {
        if (player.TryGetComponent(out ShootingController shootCtrl))
        {
            shootCtrl.SetProjectile(projectilePrefab);
        }
    }

    public string GetHoverText()
    {
        return $"Equip {projectilePrefab.name}";
    }
}
