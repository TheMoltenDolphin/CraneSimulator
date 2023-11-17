using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UltimateXR.Extensions.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClawScript : MonoBehaviour
{
    [SerializeField] private bool IsBig;
    [SerializeField] private Transform WheelPivot;
     float Zpos;


    bool stateup = true;
    bool Go;



    private void Start()
    {
        Zpos = transform.localPosition.z;
    }
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
                        CraneRotation.singleton.IsAnimated = true;
                       // collision.GetComponent<Rigidbody>().isKinematic = true;
                        collision.gameObject.transform.SetParent(WheelPivot);
                        collision.gameObject.transform.localPosition = Vector3.zero;
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
                        CraneRotation.singleton.IsAnimated = true;
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
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Untagged"))
        {
            if (IsBig && !other.gameObject.CompareTag("WheelSet"))
            {
                OutlineManager.singleton.SetGreenOutline(other.gameObject.GetComponent<Outline>());
            }
            else if (!IsBig && other.gameObject.CompareTag("WheelSet"))
            {
                OutlineManager.singleton.SetGreenOutline(other.gameObject.GetComponent<Outline>());
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
         if (!other.gameObject.CompareTag("Untagged"))
         {
            if (IsBig && !other.gameObject.CompareTag("WheelSet"))
            {
                OutlineManager.singleton.SetEmptyOutline(other.gameObject.GetComponent<Outline>());
            }
            else if (!IsBig & other.gameObject.CompareTag("WheelSet"))
            {
                OutlineManager.singleton.SetEmptyOutline(other.gameObject.GetComponent<Outline>());
            }
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!IsBig )
        {
            if (collision.gameObject.layer != 9)
            {
                if(collision.gameObject.layer != 10)
                {
                    CraneRotation.singleton.IsCollided = true;
                    Taskbar.singleton.PrintError("Зафиксировано столкновение! \n Поднимите крюк и постарайтесь больше не врезаться в посторонние объекты!");
                    print(collision.gameObject.name);
                }
            }

        }
        else if (IsBig)
        {
            if(collision.gameObject.layer != 9)
            {
                if(collision.gameObject.layer != 11)
                {
                    print(collision.gameObject.name);
                    CraneRotation.singleton.IsCollided = true;
                    Taskbar.singleton.PrintError("Зафиксировано столкновение! \n Поднимите магнит и постарайтесь больше не врезаться в посторонние объекты!");
                }
            }


        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!IsBig && (collision.gameObject.layer == 0 | !collision.gameObject.CompareTag("WheelSet")))
        {
            CraneRotation.singleton.IsCollided = false;
        }
        else if (IsBig && (collision.gameObject.layer == 0 | collision.gameObject.CompareTag("WheelSet")))
        {
            CraneRotation.singleton.IsCollided = false;
        }
    }
    private void FixedUpdate()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Zpos);
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
