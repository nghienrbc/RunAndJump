using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets instance;
    private void Awake()
    {
        instance = this;
    }
    public static GameAssets GetInstance()
    {
        return instance;
    }

    public SoundAudioClip[] soundAudioClipArray;
    public GameObject bullet;

    [Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
}
