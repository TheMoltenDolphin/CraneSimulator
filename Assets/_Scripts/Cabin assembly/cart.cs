using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cart : ParentObjectsOnPlace
{
    public int WheelCounter;
    private bool IsParented;
    private Transform Childs;
    [SerializeField] private VagonFame VagonFame;

    private void Start()
    {
        Childs = gameObject.transform.GetChild(0);
        Childs.gameObject.SetActive(false);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
    private void FixedUpdate()
    {
        if (WheelCounter == 2 && !IsParented)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<MeshCollider>().enabled = true;
            Childs.gameObject.SetActive(true);

        }
    }
    public override void OnReleaseObject(GameObject other)
    {
        IsParented = true;
        VagonFame.cartCounter++;
        if(VagonFame.cartCounter == 1)
        {
            Taskbar.singleton.PrintText("Собрать вторую тележку");
        }
        if(VagonFame.cartCounter == 2)
        {
            Taskbar.singleton.PrintText("Поставить раму на тележки");
        }
        base.OnReleaseObject(other);
        Childs.gameObject.SetActive(false);

    }
}
