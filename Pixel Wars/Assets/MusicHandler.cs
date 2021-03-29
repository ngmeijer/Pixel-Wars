using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicHandler : MonoBehaviour
{
    private AudioSource musicSource;
    private AudioMixer mixer;
    
    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
    }

    public void SetVolumeLevel(float pValue)
    {
        mixer.SetFloat("MusicVolume",Mathf.Log10(pValue) * 20);
    }
}
