﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource BackgroundSource;

    public void SetMusicVolume(float volume)
    {
        BackgroundSource.volume = volume;
    }
}
