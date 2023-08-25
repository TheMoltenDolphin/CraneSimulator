using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class Taskbar : MonoBehaviour
{
    public static Taskbar singleton { get; private set; }

    public TextMeshProUGUI[] texts;

    public GameObject ErrorObj;
    public TextMeshProUGUI errorText;
    private void Awake()
    {
        singleton = this;
    }
    public void SetTaskbarDefaultState()
    {
        for(int i = 0; i < texts.Length; i++)
        {
            texts[i].text = null;
        }
        PrintText("Собрать первую тележку");
    }
    public void PrintText(string text)
    {
        
        for (int i = texts.Length - 1; i > 0; i--)
        {
            texts[i].text = texts[i - 1].text;
        }
        texts[0].text = '-' + text;
        AudioManager.singleton.PlayAudio("Taskbar");
    }

    public void PrintError(string ErrorText)
    {
        StartCoroutine(Error(ErrorText));
    }

    public IEnumerator Error(string text)
    {
        errorText.text = text;
        ErrorObj.SetActive(true);
        AudioManager.singleton.PlayAudio("Error");
        yield return new WaitForSeconds(4);
        ErrorObj.SetActive(false);
    }
}