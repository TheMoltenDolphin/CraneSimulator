using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltimateXR.Manipulation;
using Valve.VR;
using Unity.VisualScripting;

public class CraneRotation : MonoBehaviour
{
    public static CraneRotation singleton { get; private set; }

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
    [HideInInspector] public bool IsCatched;
    [SerializeField] private SteamVR_Action_Boolean buttonTouch;



    private bool ReleaseBtn;
    private Transform TargetPosClaw;
    private bool CatchBtn;
    private Rigidbody currentObj;

    private void Awake()
    {
        singleton = this;
    }
    private void FixedUpdate()
    {
        #region CraneMovement
      //  print(JoystickUp.rotation.x);
        //if(JoystickMovement.GetComponent<UxrGrabbableObject>().IsBeingGrabbed == true)
        //{
        if(JoystickMovement.localRotation.z < 0)
        {
            TargetPosCrane = FixPoints[1];
        }
        else
        {
            TargetPosCrane = FixPoints[0];
        }
        Cabin.position = Vector3.MoveTowards(Cabin.position, TargetPosCrane.position, Mathf.Abs(JoystickMovement.transform.localRotation.z) * Speed);
        //}
        #endregion

        #region ClawMovement
        Claw.velocity = new Vector3(0, JoystickUp.localRotation.x * -1 * ClawSpeedUPDOWN, 0);
        if (JoystickRotating.localRotation.x < 0)
        {
            TargetPosClaw = ClawFixPoints[0];
        }
        else
        {
            TargetPosClaw = ClawFixPoints[1];
        }
        Claw.transform.parent.transform.parent.position = Vector3.MoveTowards(Claw.transform.parent.transform.parent.position, TargetPosClaw.position, Mathf.Abs(JoystickRotating.localRotation.x) * ClawSpeedFRONTBACK);

        if (JoystickUp.GetComponent<UxrGrabbableObject>().IsBeingGrabbed & (buttonTouch.stateDown | OVRInput.Get(OVRInput.Button.Two) && IsCatched))
        {
            ReleaseObject();
            print("works!");
        }
        #endregion

        #region ClawRotating
        if(JoystickRotating.localRotation.z  != 0)
        {
            Claw.transform.parent.transform.parent.transform.Rotate(0, JoystickRotating.localRotation.z * ClawSpeedFRONTBACK, 0);
        }
        #endregion
    }

    #region ClawCatch
    public void CacthObject(Rigidbody Object, Rigidbody Claw)
    {
        Object.AddComponent<FixedJoint>();
        Object.GetComponent<FixedJoint>().connectedBody = Claw;
        Object.useGravity = false;
        IsCatched = true;
        currentObj = Object;
    }

    [ContextMenu("Отпускание!")]
    public void ReleaseObject()
    {
        currentObj.useGravity = true;
        IsCatched = false;
        Destroy(currentObj.GetComponent<FixedJoint>());
    }
    #endregion
}
