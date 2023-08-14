using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager singleton;
    public List<AudioObject> Audios = new List<AudioObject>();
    private AudioSource audioSource;

    private void Awake()
    {
        singleton = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(string name)
    {
        for(int i = 0; i < Audios.Count; i++)
        {
            if(Audios[i].name == name)
            {
                audioSource.Play();
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
