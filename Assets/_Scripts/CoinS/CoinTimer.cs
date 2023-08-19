using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTimer : MonoBehaviour
{
    [SerializeField] private float timer = 60;
    void Update()
    {
        if(gameObject.transform.GetChild(0).gameObject.activeSelf == false)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                SetCoinDefaultState();
            }
        }
    }

    public void SetCoinDefaultState()
    {
        timer = 60;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
