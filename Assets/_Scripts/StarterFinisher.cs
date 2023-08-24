using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterFinisher : MonoBehaviour
{
    public static StarterFinisher instance;
    [SerializeField] private CoinTimer[] coins;
    [SerializeField] private GameObject TrainPrefab;
    [SerializeField] private Transform PrefabDest;
    [SerializeField] private ParticleSystem BigSmoke;
    
    private GameObject NewTrain;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
      //  startGame();
    }
    [ContextMenu("ResetGame!")]
    public void startGame()
    {
        CoinSpawn.singleton.StartGame();
        StartCoroutine(SpawnTrain());  
    }

    public IEnumerator SpawnTrain()
    {
        CraneRotation.singleton.SetDefaultState();
        CoinSpawn.singleton.IsPlaying = true;
        CraneRotation.singleton.IsGamePlaying = true;
        BigSmoke.Play();
        if (NewTrain != null)
        {
            Destroy(NewTrain);
        }
        GameObject.Find("HelperCanv").SetActive(true);
        AudioManager.singleton.PlayAudio("LongSteam");
        yield return new WaitForSeconds(BigSmoke.main.duration / 2);
        for(int i = 0; i < coins.Length; i++)
        {
            coins[i].SetCoinDefaultState();
        }
        CoinSpawn.singleton.CoinCounter = 0;
        Taskbar.singleton.SetTaskbarDefaultState();
        NewTrain = Instantiate(TrainPrefab, PrefabDest.position, PrefabDest.rotation);
        OutlineManager.singleton.SetObjects();
    }
    [ContextMenu("FinishGame")]
    public void EndGame()
    {
        GameObject.FindWithTag("StartBtn").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("HelperCanv").SetActive(false);
        CraneRotation.singleton.IsGamePlaying = false;
    }

    
}
