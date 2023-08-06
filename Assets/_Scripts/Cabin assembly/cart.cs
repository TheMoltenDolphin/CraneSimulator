using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cart : ParentObjectsOnPlace
{
    public int WheelCounter;
    private bool IsParented;
    [SerializeField] private FinishedWagon FinishedWagon;

    private void Start()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
    private void FixedUpdate()
    {
        if (WheelCounter == 2 && !IsParented)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<MeshCollider>().enabled = true;
        }
    }
    public override void OnReleaseObject(GameObject other)
    {
        IsParented = true;
        FinishedWagon.cartCounter++;
        if(FinishedWagon.cartCounter == 1)
        {
            Taskbar.singleton.PrintText("Собрать вторую тележку");
        }
        if(FinishedWagon.cartCounter == 2)
        {
            Taskbar.singleton.PrintText("Поставить бочку на тележки");
        }
        base.OnReleaseObject(other);
        
    }
}
