using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltimateXR.Manipulation;

public class SetJoystickDefaultState : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (gameObject.GetComponent<UxrGrabbableObject>().IsBeingGrabbed != true)
        {
            gameObject.GetComponent<UxrGrabbableObject>().ResetPositionAndState(true);
        }

    }
}
