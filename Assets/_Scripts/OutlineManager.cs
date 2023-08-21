using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineManager : MonoBehaviour
{
    public static OutlineManager singleton;

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
    private void Awake()
    {
        singleton = this;
    }
     
    public void SetGreenOutline(Outline outline)
    {
        outline.OutlineColor = green;
        outline.OutlineWidth = Width;
    }

    public void SetEmptyOutline(Outline outline)
    {
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = white;
        outline.OutlineWidth = FreeWidth;
    }

    public void SetCatchedOutline(Outline outline)
    {
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
