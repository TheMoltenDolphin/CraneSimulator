using System.Collections;
using System.Collections.Generic;
using UltimateXR.Devices.Integrations.Oculus;
using UnityEngine;

public class ParentObjectsOnPlace : MonoBehaviour
{
    [SerializeField] private string Comparetag;
    [SerializeField] private GameObject NextPivot;
    [SerializeField] private Transform parent;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(Comparetag))
        {
            //other.gameObject.AddComponent<FixedJoint>();
            //other.gameObject.GetComponent<FixedJoint>().connectedBody = gameObject.GetComponent<Rigidbody>();
            Destroy(other.gameObject.GetComponent<FixedJoint>());
            Destroy(other.gameObject.GetComponent<Rigidbody>());
            other.transform.position = gameObject.transform.position;
            other.transform.rotation = gameObject.transform.rotation;
            NextPivot.SetActive(true);

        }
    }
}
