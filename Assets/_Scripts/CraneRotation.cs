using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltimateXR.Manipulation;
using Valve.VR;

public class CraneRotation : MonoBehaviour
{
    [Header("Джойстики")]
    [SerializeField] private Transform JoystickUp;
    [SerializeField] private Transform JoystickRotating;
    [SerializeField] private Transform JoystickMovement;
    [SerializeField] private float Speed;
    
    [Header("Кабина")]
    [SerializeField] private Transform Cabin;
    [SerializeField] private Transform[] FixPoints;
    private Transform TargetPosCrane;

    [Header("Клешня")]
    [SerializeField] private Rigidbody Claw;
    [SerializeField] private Transform[] ClawFixPoints;
    [SerializeField] private float ClawSpeedUPDOWN;
    [SerializeField] private float ClawSpeedFRONTBACK;
    private Transform TargetPosClaw;


    [SerializeField] private SteamVR_Action_Boolean buttonTouch;

    private void FixedUpdate()
    {
        #region CraneMovement
      //  print(JoystickUp.rotation.x);
        //if(JoystickMovement.GetComponent<UxrGrabbableObject>().IsBeingGrabbed == true)
        //{
        if(JoystickMovement.localRotation.z < 0)
        {
            TargetPosCrane = FixPoints[0];
        }
        else
        {
            TargetPosCrane = FixPoints[1];
        }
        Cabin.position = Vector3.MoveTowards(Cabin.position, TargetPosCrane.position, Mathf.Abs(JoystickMovement.transform.localRotation.z) * Speed);
        //}
        #endregion

        #region ClawMovement
        Claw.velocity = new Vector3(0, JoystickUp.rotation.x * -1 * ClawSpeedUPDOWN, 0);
        if (JoystickRotating.localRotation.x < 0)
        {
            TargetPosClaw = ClawFixPoints[0];
        }
        else
        {
            TargetPosClaw = ClawFixPoints[1];
        }
        Claw.transform.parent.transform.parent.position = Vector3.MoveTowards(Claw.transform.parent.transform.parent.position, TargetPosClaw.position, Mathf.Abs(JoystickRotating.localRotation.x) * ClawSpeedFRONTBACK);

        if (JoystickUp.GetComponent<UxrGrabbableObject>().IsBeingGrabbed & (buttonTouch.stateDown | OVRInput.Get(OVRInput.Button.Two)))
        {
            print("works!");
        }
        #endregion


    }
}
