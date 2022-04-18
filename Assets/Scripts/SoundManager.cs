using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    private bool _mute;
    private void Awake()
    {
        if (PlayerPrefs.GetInt("Mute") == 1)
        {
            _mute = true;
            audioMixer.SetFloat("Volume", -80);
        }
        else
        {
            _mute = false;
            audioMixer.SetFloat("Volume", 0);
        }
    }
}
