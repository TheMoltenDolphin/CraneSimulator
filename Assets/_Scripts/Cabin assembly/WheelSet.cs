using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSet : ParentObjectsOnPlace
{
    private cart cart;

    private void Start()
    {
        cart = gameObject.transform.parent.GetComponent<cart>();
    }
    public override void OnReleaseObject(GameObject other)
    {
        cart.WheelCounter++;
        base.OnReleaseObject(other);
    }
} 
