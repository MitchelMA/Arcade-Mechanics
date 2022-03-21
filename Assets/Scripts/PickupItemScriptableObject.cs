using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PickupItem", order = 1)]
public class PickupItemScriptableObject : ScriptableObject
{
    [SerializeField]
    private string itemName;
    public string ItemName => itemName;
}
