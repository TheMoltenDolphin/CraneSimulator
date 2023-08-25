using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UltimateXR.Manipulation;
using Valve.VR;
using Unity.VisualScripting;
using UnityEditor;
using System;
using System.ComponentModel.Composition;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;

public class CraneRotation : MonoBehaviour
{
    public static CraneRotation singleton { get; private set; }

    [Header("Джойстики")]
    [SerializeField] private Transform JoystickRotating;
    [SerializeField] private Transform JoystickMovement;
    [SerializeField] private float Speed;
    
    [Header("Кабина")]
    [SerializeField] private Transform Cabin;
    [SerializeField] private Transform[] FixPoints;
    private Transform TargetPosCrane;
    [SerializeField] private AudioSource engineSound;

    [Header("Клешня")]
    [SerializeField] private Transform ClawParent;
    public Claw[] ClawList;
    [SerializeField] private Transform[] ClawFixPoints;
    [SerializeField] private float ClawSpeedFRONTBACK;
    [SerializeField] private AudioSource ClawFrontBack;
    [SerializeField] private AudioSource ClawUpDown;
    public SteamVR_Action_Boolean buttonTouch;
    [SerializeField] private AnimationClip ClawAnim;



    private bool ReleaseBtn;
    public bool IsAnimated;
    public bool IsGamePlaying;
    private Transform TargetPosClaw;
    public bool CatchReleaseBtn;
    public bool IsCollided;
    private void Awake()
    {
        singleton = this;
    }
    private void FixedUpdate()
    {
        if (!IsAnimated && IsGamePlaying)
        {
            #region CraneMovement
            if (JoystickMovement.GetComponent<UxrGrabbableObject>().IsBeingGrabbed == true && !IsCollided)
            {
                if (JoystickMovement.localRotation.x > 0)
                {
                    TargetPosCrane = FixPoints[1];
                }
                else
                {
                    TargetPosCrane = FixPoints[0];
                }
                Cabin.position = Vector3.MoveTowards(Cabin.position, TargetPosCrane.position, Mathf.Abs(JoystickMovement.transform.localRotation.x) * Speed);
            }
            engineSound.volume = Mathf.Abs(JoystickMovement.transform.localRotation.x) * 1.2f;
            #endregion

            #region ClawMovement
            ClawList[0].ClawRB.velocity = new Vector3(0, ClawList[0].ClawController.localRotation.x * -1 * ClawList[0].ClawSpeedUPDOWN, 0);
            ClawUpDown.volume = Mathf.Max(Mathf.Abs(ClawList[0].ClawController.localRotation.x), Mathf.Abs(ClawList[1].ClawController.localRotation.x)) * 2;
            ClawList[1].ClawRB.velocity = new Vector3(0, ClawList[1].ClawController.localRotation.x * -1 * ClawList[1].ClawSpeedUPDOWN, 0);
            if (!IsCollided)
            {
                if (JoystickRotating.localRotation.x > 0)
                {
                    TargetPosClaw = ClawFixPoints[0];
                }
                else
                {
                    TargetPosClaw = ClawFixPoints[1];
                }
                ClawParent.position = Vector3.MoveTowards(ClawParent.position, TargetPosClaw.position, Mathf.Abs(JoystickRotating.localRotation.x) * ClawSpeedFRONTBACK);
            }
            ClawFrontBack.volume = Mathf.Abs(JoystickRotating.localRotation.x) * 2;
            if (ClawList[0].ClawController.GetComponent<UxrGrabbableObject>().IsBeingGrabbed & (buttonTouch.stateDown | OVRInput.Get(OVRInput.Button.Two) && ClawList[0].IsCatched))
            {
                ReleaseObject(true);
            }
            else if (ClawList[1].ClawController.GetComponent<UxrGrabbableObject>().IsBeingGrabbed & (buttonTouch.stateDown | OVRInput.Get(OVRInput.Button.Two) && ClawList[1].IsCatched))
            {
                ReleaseObject(false);
            }
            #endregion
        }
        else
        {
            ClawList[0].ClawRB.velocity = Vector3.zero;
            ClawList[1].ClawRB.velocity = Vector3.zero;
        }
    }

    #region ClawCatch
    public void CacthObject(Rigidbody Object, bool IsBig)
    {
        AudioManager.singleton.PlayAudio("Steam");
        AudioManager.singleton.PlayAudio("Molot");
        if (IsBig)
        {
            ClawList[0].IsCatched = true;
        }
        else
        {
            ClawList[1].IsCatched = true;
        }
        StartCoroutine(Catcher(Object, IsBig));
    }
    public void SetDefaultState()
    {
        ClawList[1].IsCatched = false;
        ClawList[0].IsCatched = false;

    }
    public IEnumerator Catcher(Rigidbody Object, bool IsBig)
    {
        IsAnimated = true;
        if (IsBig)
        {
            ClawList[0].Particles.Play();
            //ClawList[0].ClawRB.GetComponent<Animator>().SetTrigger("Go!");
            yield return new WaitForSeconds(ClawAnim.length * 0.8f);
            Object.AddComponent<FixedJoint>();
            Object.GetComponent<FixedJoint>().connectedBody = ClawList[0].ClawRB;
            ClawList[0].CatchedObject = Object.gameObject.GetComponent<Rigidbody>();
            OutlineManager.singleton.SetCatchedOutline(Object.GetComponent<Outline>());
        }
        else
        {
            ClawList[1].Particles.Play();
            //ClawList[1].ClawRB.GetComponent<Animator>().SetTrigger("Go!");
            Object.AddComponent<FixedJoint>();
            Object.GetComponent<FixedJoint>().connectedBody = ClawList[1].ClawRB;
            yield return new WaitForSeconds(ClawAnim.length * 0.8f);
            ClawList[1].CatchedObject = Object.gameObject.GetComponent<Rigidbody>();
            OutlineManager.singleton.SetCatchedOutline(Object.GetComponent<Outline>());
            Object.isKinematic = false;

        }
        Object.useGravity = false;
        OutlineManager.singleton.SetCatchedOutline(Object.GetComponent<Outline>());
        Object.GetComponent<Outline>().needsUpdate = true;
        IsAnimated = false;
    }
    public IEnumerator Releaser(bool IsBig)
    {        
        IsAnimated = true;
        if (IsBig)
        {
            ClawList[0].Particles.Play();
            //ClawList[0].ClawRB.GetComponent<Animator>().SetTrigger("Back");
            AudioManager.singleton.PlayAudio("Steam");
            yield return new WaitForSeconds(ClawAnim.length * 0.8f);
            ClawList[0].CatchedObject.useGravity = true;
            OutlineManager.singleton.SetEmptyOutline(ClawList[0].CatchedObject.GetComponent<Outline>());
            ClawList[0].CatchedObject.GetComponent<Outline>().needsUpdate = true;
            ClawList[0].IsCatched = false;
            Destroy(ClawList[0].CatchedObject.GetComponent<FixedJoint>());
            ClawList[0].CatchedObject.transform.parent = null;
            ClawList[0].CatchedObject = null;
        }
        else
        {
            ClawList[1].Particles.Play();
            //ClawList[1].ClawRB.GetComponent<Animator>().SetTrigger("Back");
            AudioManager.singleton.PlayAudio("Steam");
            yield return new WaitForSeconds(ClawAnim.length * 0.85f);
            ClawList[1].CatchedObject.useGravity = true;
            OutlineManager.singleton.SetEmptyOutline(ClawList[1].CatchedObject.GetComponent<Outline>());
            ClawList[1].CatchedObject.GetComponent<Outline>().needsUpdate = true;
            ClawList[1].IsCatched = false;
            Destroy(ClawList[1].CatchedObject.GetComponent<FixedJoint>());
            ClawList[1].CatchedObject.transform.parent = null;
            ClawList[1].CatchedObject = null;
            

        }
        
        IsAnimated = false;

    }
    [ContextMenu("Отпускание!")]
    public void ReleaseObject(bool IsBig = false)
    {
        StartCoroutine(Releaser(IsBig));

    }
    #endregion
}

[Serializable]
public class Claw
{
    public bool IsColliding;
    public bool IsCatched;
    public Transform ClawController;
    public ParticleSystem Particles;
    public Rigidbody ClawRB;
    public float ClawSpeedUPDOWN;
    public Rigidbody CatchedObject;
}