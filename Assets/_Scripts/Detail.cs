using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detail : MonoBehaviour
{
    public bool IsBig;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer != 9)
        {
            if(collision.gameObject.layer != 13)
            {
                CraneRotation.singleton.IsCollided = true;
                print(gameObject.layer);
                Taskbar.singleton.PrintError("Зафиксировано столкновение! \n Поднимите деталь и постарайтесь больше не врезаться в посторонние объекты!");
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer != 9 && collision.gameObject.layer != 7 && collision.gameObject.layer != 13)
        {
            if(collision.gameObject.layer != 13)
            {
                CraneRotation.singleton.IsCollided = false;
            }

        }
    }
}
