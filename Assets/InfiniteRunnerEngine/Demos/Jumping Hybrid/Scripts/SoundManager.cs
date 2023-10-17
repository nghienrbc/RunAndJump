using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
        Jump,
        Fire,
        Died,
        EnemyDied,
        ButtonOver,
        ButtonClick,
    }

    public static void PlaySound(Sound sound)
    {
        GameObject gameObj = new GameObject("Sound", typeof(AudioSource));
        AudioSource audioSource = gameObj.GetComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
        //GameObject.Destroy(audioSource);
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.GetInstance().soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found");    
        return null;
    }
     
}
