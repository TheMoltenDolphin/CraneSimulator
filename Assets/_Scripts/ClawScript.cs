using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UltimateXR.Extensions.Unity;
using UnityEngine;

public class ClawScript : MonoBehaviour
{
    [SerializeField] private bool IsBig;
    private void OnTriggerEnter(Collider collision)
    {
        if (!IsBig)
        {
            if (collision.gameObject.CompareTag("WheelSet") && !CraneRotation.singleton.IsCatched)
            {
                CraneRotation.singleton.CacthObject(collision.gameObject.GetComponent<Rigidbody>(), gameObject.GetComponent<Rigidbody>());
                collision.gameObject.GetComponent<Rigidbody>().useGravity = false;
            }
            else
            {
                Taskbar.singleton.PrintError("Грузоподъёмность слишком мала\nИспользуйте Кран побольше");
            }
        }
        else
        {
            if (!collision.gameObject.CompareTag("WheelSet") && !CraneRotation.singleton.IsCatched)
            {
                CraneRotation.singleton.CacthObject(collision.gameObject.GetComponent<Rigidbody>(), gameObject.GetComponent<Rigidbody>());
                collision.gameObject.GetComponent<Rigidbody>().useGravity = false;
            }
            else
            {
                Taskbar.singleton.PrintError("Грузоподъёмность слишком высока. малую деталь следует переносить малым краном");
            }
        }
    }
}
