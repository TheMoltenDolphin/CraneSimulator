using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class CoinSpawn : MonoBehaviour
{
    public static CoinSpawn singleton;
    public GameObject CoinPrefab;
    [SerializeField] private int CoinAmount;
    public float CoinCounter;
    private float time;
    [HideInInspector]public bool IsPlaying;
    [SerializeField] private TextMeshProUGUI TimerText;
    [SerializeField] private TextMeshProUGUI CollectedCoinCounter;

    private void Awake()
    {
        singleton = this;
    }
    #region FirstMethod
    //[ContextMenu("Place Coins!")]
    //public void ReplaceCoins()
    //{
    //    int[] excludes = new int[CoinAmount];
    //    for(int j = 0; j < CoinAmount; j++)
    //    {
    //        excludes[j] = 993;
    //    }
    //    for (int i = 0; i < CoinAmount; i++)
    //    {
    //        int s = RandomFromRangeWithExceptions(0, CoinSpawners.Length+1, excludes);
    //        Instantiate(CoinPrefab, CoinSpawners[s]);
    //        excludes[i] = s;
    //    }
    //}
    //int RandomFromRangeWithExceptions(int rangeMin, int rangeMax, params int[] exclude)//exclude -- список чисел которые НЕ должны входить в результат
    //{
    //    var _rand = new System.Random();
    //    var range = Enumerable.Range(rangeMin, rangeMax).Where(i => !exclude.Contains(i));//создаем  колекцию допустимых значений
    //    int index = Random.Range(rangeMin, rangeMax - exclude.Length);//генерируем индекс ячейки
    //    return range.ElementAt(index);//возвращаем значение ячейки
    //}
    #endregion

    private void FixedUpdate()
    {
        if (IsPlaying)
        {
            time += Time.fixedDeltaTime;
            TimerText.text = $"Прошло\n {Mathf.Round(time)} секунд ";
            CollectedCoinCounter.text = CoinCounter.ToString();
        }
    }
    [ContextMenu("Start!!!")]
    public void StartGame()
    {
        IsPlaying = true;
    }
}
