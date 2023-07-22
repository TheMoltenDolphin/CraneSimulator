using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Untagged" && !CraneRotation.singleton.IsCatched)
        {
            CraneRotation.singleton.CacthObject(collision.gameObject.GetComponent<Rigidbody>(), gameObject.GetComponent<Rigidbody>());
        }
    }
}
