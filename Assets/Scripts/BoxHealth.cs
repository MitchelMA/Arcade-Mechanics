using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    public int CurrentHealth { get; private set; } = 0;

    private void Start()
    {
        CurrentHealth = maxHealth;
    }
    
    
    public void Damage(int damage)
    {
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
        Debug.Log(CurrentHealth);
    }

    public bool IsAlive()
    {
        return CurrentHealth > 0;
    }
}
