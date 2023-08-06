using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedWagon : ParentObjectsOnPlace
{
    public float cartCounter;
    private bool IsParented;
    private void Start()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;

    }
    private void FixedUpdate()
    {
        if (cartCounter == 2 && !IsParented)
        {
            Taskbar.singleton.PrintText("Поместить бочку на тележки");
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<MeshCollider>().enabled = true;
        }
    }
    public override void OnReleaseObject(GameObject other)
    {
        base.OnReleaseObject(other);
        IsParented = true;
        OnVagonDone();
    }

    void OnVagonDone()
    {
        Debug.Log("Vagon is done!!!");
        Taskbar.singleton.PrintText("Отправить вагон и начать собирать следующий!");
    }
}
