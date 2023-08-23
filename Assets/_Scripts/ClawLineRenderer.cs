using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawLineRenderer : MonoBehaviour
{
    LineRenderer Main;
    [SerializeField] private Transform[] linepPos;


    private void Start()
    {
        Main = GetComponent<LineRenderer>();
    }
    private void FixedUpdate()
    {
        for(int i = 0; i < linepPos.Length; i++)
        {
            Main.SetPosition(i, linepPos[i].position);
        }
    }
}
