using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VagonFame : ParentObjectsOnPlace
{
    public float cartCounter;
    private bool IsParented;
    [SerializeField] private  FinishedWagon FinishedWagon;
    private void Start()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;

    }
    private void FixedUpdate()
    {
        if (cartCounter == 2 && !IsParented)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<Collider>().enabled = true;
        }
    }
    public override void OnReleaseObject(GameObject other)
    {
        Taskbar.singleton.PrintText("Поместить бочку на раму");
        base.OnReleaseObject(other);
        IsParented = true;
        FinishedWagon.IsPlatformPlaced = true;
    }

}
