using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Search;
using UnityEngine.SearchService;

public class CoinSlave : MonoBehaviour
{
    public string InteractionTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(InteractionTag))
        {
            CoinSpawn.singleton.CoinCounter++;
        }
    }
}
