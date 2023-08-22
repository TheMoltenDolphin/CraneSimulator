using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

public class FinishedWagon : ParentObjectsOnPlace
{
    public bool IsPlatformPlaced;
    private bool IsParented;
    private void Start()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;

    }
    private void FixedUpdate()
    {
        if (IsPlatformPlaced && !IsParented)
        {
            IsPlatformPlaced = false;
            Taskbar.singleton.PrintText("Поместить бочку на тележки");
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<Collider>().enabled = true;
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
        GameObject.FindWithTag("FinishBtn").transform.GetChild(0).gameObject.SetActive(true);
        Taskbar.singleton.PrintText("Отправить вагон и начать собирать следующий!");
    }
}
