using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OutlineManager : MonoBehaviour
{
    public static OutlineManager singleton;
    bool canister;
    bool connector;
    bool cart;


    [Header("Ресурсы")]
    [SerializeField] private GameObject[] wheels;
    [SerializeField] private GameObject[] carts;
    [SerializeField] private GameObject cistern;
    [SerializeField] private GameObject connectorObj;

    [Header("Коллайдеры")]
    [SerializeField] private cart[] cartScript;
    [SerializeField] private FinishedWagon cisternScript;
    [SerializeField] private VagonFame connectorScript;


    [Header("цвет объекта для захвата")]
    [SerializeField] private Color green;
    [SerializeField] private float Width;

    [Header("цвет свободного объекта")]
    [SerializeField] private Color white;
    [SerializeField] private float FreeWidth;

    [Header("цвет захваченного объекта")]
    [SerializeField] private Color CatchedRed;
    [SerializeField] private Color CatchedWhite;
    [SerializeField] private float CatchedWidth;
    
    public void SetObjects()
    {
        cartScript = FindObjectsOfType<cart>();
        cisternScript = FindObjectOfType<FinishedWagon>();
        connectorScript = FindObjectOfType<VagonFame>();
        wheels = GameObject.FindGameObjectsWithTag("WheelSet");
        carts = GameObject.FindGameObjectsWithTag("Cart");
        cistern = GameObject.FindGameObjectWithTag("Vagon");
        connectorObj = GameObject.FindGameObjectWithTag("Platform");
        for(int i = 0; i < wheels.Length; i++)
        {
            SetEmptyOutline(wheels[i].GetComponent<Outline>());
        }
    }

    private void FixedUpdate()
    {
        if(!cart && (cartScript[0].WheelCounter == 2 | cartScript[1].WheelCounter == 2))
        {
            for(int i = 0; i < carts.Length; i++)
            {
                SetEmptyOutline(carts[i].GetComponent<Outline>());
            }
            cart = true;
        }

        if(!connector & connectorScript.cartCounter == 2)
        {
            connector = true;
            SetEmptyOutline(connectorObj.GetComponent<Outline>());
        }

        if(!canister && cisternScript.IsPlatformPlaced)
        {
            canister = true;
            SetEmptyOutline(cistern.GetComponent<Outline>());
        }
    }

    private void Awake()
    {
        singleton = this;
    }
     
    public void SetGreenOutline(Outline outline)
    {
        outline.OutlineColor = green;
        outline.OutlineWidth = Width;
        outline.enabled = true;
        outline.OutlineMode = Outline.Mode.OutlineVisible;

    }

    public void SetEmptyOutline(Outline outline)
    {
        outline.enabled = true;
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = white;
        outline.OutlineWidth = FreeWidth;
    }

    public void SetCatchedOutline(Outline outline)
    {
        outline.enabled = true;
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = CatchedWhite;
        outline.OutlineInvisibleColor = CatchedRed;
        outline.OutlineWidth = CatchedWidth;
    }

    public void DisableOutline(Outline outline)
    {
        outline.enabled = false;
    }
}
