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
        AudioManager.singleton.PlayAudio("Coin");
        CoinSpawn.singleton.CoinCounter++;
        gameObject.SetActive(false);
    }
}
