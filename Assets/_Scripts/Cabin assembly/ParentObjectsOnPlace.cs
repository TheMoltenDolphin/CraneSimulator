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
            OnReleaseObject(other.gameObject);
            //other.gameObject.AddComponent<FixedJoint>();
            //other.gameObject.GetComponent<FixedJoint>().connectedBody = gameObject.GetComponent<Rigidbody>();
        }
    }
    
    public virtual void OnReleaseObject(GameObject other)
    {
        other.gameObject.tag = "Untagged";
        Destroy(other.gameObject.GetComponent<FixedJoint>());
        Destroy(other.gameObject.GetComponent<Rigidbody>());
        other.transform.position = gameObject.transform.position;
        other.transform.rotation = gameObject.transform.rotation;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<MeshCollider>().enabled = false;
    }
}
