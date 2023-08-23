using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager singleton;
    public List<AudioObject> Audios = new List<AudioObject>();
    [SerializeField] private GameObject AudioObject;

    private void Awake()
    {
        singleton = this;
    }

    public void PlayAudio(string name, float volume = 1)
    {
        for(int i = 0; i < Audios.Count; i++)
        {
            if(Audios[i].name == name)
            {
                AudioSource Object = Instantiate(AudioObject).GetComponent<AudioSource>();
                Object.clip = Audios[i].AudioClip;
                Object.volume = volume;
                Object.Play();
                Object.GetComponent<self_destroy>().StartCoroutine(Object.GetComponent<self_destroy>().SelfDestroy());
            }
        }
    }
}
[Serializable]
public class AudioObject
{
    public string name;
    public AudioClip AudioClip;
}
