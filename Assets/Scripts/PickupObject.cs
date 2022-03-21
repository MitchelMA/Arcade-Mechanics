using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    [SerializeField] private PickupItemScriptableObject obj;
    public PickupItemScriptableObject Obj => obj;
}
