using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltimateXR.Manipulation;

public class CraneRotation : MonoBehaviour
{
    [SerializeField] private Transform JoystickUp;
    [SerializeField] private Transform JoystickRotating;
    [SerializeField] private Transform JoystickMovement;
    [SerializeField] private float JoystickSpeed;

    [SerializeField] private Transform Cabin;
    [SerializeField] private Transform Claw;
    [SerializeField] private Transform[] FixPoints;
    private Transform endPos;

    private void FixedUpdate()
    {
        #region CraneMovement
        print(JoystickMovement.transform.localRotation.z);
        //if(JoystickMovement.GetComponent<UxrGrabbableObject>().IsBeingGrabbed == true)
        //{
        if(JoystickMovement.transform.localRotation.z < 0)
        {
            endPos = FixPoints[1];
        }
        else
        {
            endPos = FixPoints[0];
        }
        Cabin.position = Vector3.MoveTowards(Cabin.position, endPos.position, Mathf.Abs(JoystickMovement.transform.localRotation.z) * JoystickSpeed);
        //}
        #endregion

        #region ClawMovement

        #endregion
    }
}
