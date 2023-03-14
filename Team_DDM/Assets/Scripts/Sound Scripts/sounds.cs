using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class sounds
{
    public enum AudioCat
    {
        Music = 1,
        SoundeEffects = 2
    }

    public AudioCat audioType;
    [HideInInspector] public AudioSource soundSource;
    public AudioClip soundClip;
    public string soundName;
    public bool isLooping;
    public bool onWake;
    [Range(0, 1)] public float volume;

}
