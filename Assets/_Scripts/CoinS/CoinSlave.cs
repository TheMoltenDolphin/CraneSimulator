using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Search;
using UnityEngine.SearchService;

public class CoinSlave : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CoinSpawn.singleton.CoinCounter++;
        gameObject.transform.parent.gameObject.SetActive(false);

    }
}
