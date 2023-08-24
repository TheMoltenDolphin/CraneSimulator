using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UltimateXR.Extensions.Unity;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClawScript : MonoBehaviour
{
    [SerializeField] private bool IsBig;
    [SerializeField] private Transform CartPivot;
    [SerializeField] private Transform WheelPivot;


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
                        CraneRotation.singleton.IsAnimated = true;
                        collision.GetComponent<Rigidbody>().isKinematic = true;
                        collision.gameObject.transform.position = WheelPivot.position;
                        collision.gameObject.transform.rotation = WheelPivot.rotation;
                        collision.gameObject.transform.SetParent(WheelPivot);
                        collision.gameObject.AddComponent<FixedJoint>().connectedBody = gameObject.GetComponent<Rigidbody>();
                        collision.GetComponent<Rigidbody>().isKinematic = false;
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
                        if (collision.gameObject.CompareTag("Cart"))
                        {

                            collision.GetComponent<Rigidbody>().isKinematic = true;
                            collision.gameObject.transform.position = CartPivot.position;
                            collision.gameObject.transform.rotation = CartPivot.rotation;
                            collision.gameObject.transform.SetParent(WheelPivot);
                            collision.gameObject.AddComponent<FixedJoint>().connectedBody = gameObject.GetComponent<Rigidbody>();
                            collision.GetComponent<Rigidbody>().isKinematic = false;
                            collision.gameObject.transform.position = CartPivot.position;
                        }
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

        else if (collision.gameObject.CompareTag("Untagged") && (collision.gameObject.layer != 7 | collision.gameObject.layer != 9))
        {
            Taskbar.singleton.PrintError("Вы столкнулись с посторонней поверхностью!!!");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Untagged"))
        {
            OutlineManager.singleton.SetGreenOutline(other.gameObject.GetComponent<Outline>());
            if (IsBig)
            {
                CraneRotation.singleton.ClawList[0].IsColliding = true;
            }
            else if (!IsBig)
            {
                CraneRotation.singleton.ClawList[1].IsColliding = true;
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
         if (!other.gameObject.CompareTag("Untagged"))
         {
            OutlineManager.singleton.SetEmptyOutline(other.gameObject.GetComponent<Outline>());
            if (IsBig)
            {
                CraneRotation.singleton.ClawList[0].IsColliding = false;
            }
            else if (!IsBig)
            {
                CraneRotation.singleton.ClawList[1].IsColliding = false;
            }
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
