using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UltimateXR.Extensions.Unity;
using UnityEngine;

public class ClawScript : MonoBehaviour
{
    [SerializeField] private bool IsBig;
    bool stateup;
    bool Go;
    private void OnTriggerStay(Collider collision)
    {
        if (!collision.gameObject.CompareTag("Untagged") && Go)
        {
            Go = false;
            if (!IsBig)
            {
                if (collision.gameObject.CompareTag("WheelSet"))
                {
                    if (!CraneRotation.singleton.ClawList[1].IsCatched)
                    {
                        CraneRotation.singleton.CacthObject(collision.gameObject.GetComponent<Rigidbody>(), false);
                        collision.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    }
                }
                else
                {
                    Taskbar.singleton.PrintError("Грузоподъёмность слишком мала\nИспользуйте Кран побольше");
                }
            }
            else
            {
                if (!collision.gameObject.CompareTag("WheelSet"))
                {
                    if (!CraneRotation.singleton.ClawList[0].IsCatched)
                    {
                        CraneRotation.singleton.CacthObject(collision.gameObject.GetComponent<Rigidbody>(), true);
                        collision.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    }
                }
                else
                {
                    Taskbar.singleton.PrintError("Грузоподъёмность слишком высока\nмалую деталь следует переносить малым краном");
                }
            }
        }

        else if (collision.gameObject.CompareTag("Untagged"))
        {
            Taskbar.singleton.PrintError("Вы столкнулись с посторонней поверхностью!!!");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Untagged"))
        {
            OutlineManager.singleton.SetGreenOutline(other.gameObject.GetComponent<Outline>());
        }  
    }
    private void OnTriggerExit(Collider other)
    {
         if (!other.gameObject.CompareTag("Untagged"))
         {
            OutlineManager.singleton.SetEmptyOutline(other.gameObject.GetComponent<Outline>());
         }
    }
    private void FixedUpdate()
    {
        if (CraneRotation.singleton.buttonTouch.stateUp)
        {
            stateup = true;
            Go = false;
        }
        if(CraneRotation.singleton.buttonTouch.stateDown && stateup)
        {
            Go = true;
            stateup = false;
        }
    }
}
