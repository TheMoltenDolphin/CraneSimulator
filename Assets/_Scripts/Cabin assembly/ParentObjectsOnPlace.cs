using System.Collections;
using System.Collections.Generic;
using UltimateXR.Devices.Integrations.Oculus;
using UnityEngine;

public abstract class ParentObjectsOnPlace : MonoBehaviour
{
    [SerializeField] private string Comparetag;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(Comparetag))
        {   
            if(other.gameObject.GetComponent<Outline>().enabled & other.gameObject.GetComponent<Outline>().OutlineWidth < 7)
            {
                OnReleaseObject(other.gameObject);
                //other.gameObject.AddComponent<FixedJoint>();
                //other.gameObject.GetComponent<FixedJoint>().connectedBody = gameObject.GetComponent<Rigidbody>();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Comparetag))
        {
            OutlineManager.singleton.SetGreenOutline(other.gameObject.GetComponent<Outline>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Comparetag))
        {
            OutlineManager.singleton.SetEmptyOutline(other.gameObject.GetComponent<Outline>());
        }
    }
    public virtual void OnReleaseObject(GameObject other)
    {
        other.gameObject.tag = "Untagged";
        Destroy(other.gameObject.GetComponent<FixedJoint>());
        Destroy(other.gameObject.GetComponent<Rigidbody>());
        OutlineManager.singleton.DisableOutline(other.GetComponent<Outline>());
        other.transform.position = gameObject.transform.position;
        other.transform.rotation = gameObject.transform.rotation;
        other.gameObject.layer = 9;
        other.transform.SetParent(GameObject.Find("VagonColliders").transform);
        Destroy(other.GetComponent<Detail>());
        AudioManager.singleton.PlayAudio("Coin");
        gameObject.SetActive(false);

    }
}
