using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class audioManager : MonoBehaviour
{
    public static audioManager instance;

    [SerializeField] AudioMixerGroup musicGroup;
    [SerializeField] AudioMixerGroup soundEffectGroup;
    [SerializeField] sounds[] soundArray;
    public const string prefAudioMute = "AudioMute";

    void Awake()
    {
        instance = this;

        if (PlayerPrefs.HasKey(prefAudioMute))
        {
            AudioListener.volume = PlayerPrefs.GetFloat(prefAudioMute);
        }

        foreach (sounds i in soundArray)
        {
            i.soundSource = gameObject.AddComponent<AudioSource>();
            i.soundSource.clip = i.soundClip;
            i.soundSource.loop = i.isLooping;
            i.soundSource.playOnAwake = i.onWake;
            i.soundSource.volume = i.volume;

            switch (i.audioType)
            {
                case sounds.AudioCat.Music:
                    i.soundSource.outputAudioMixerGroup = musicGroup;
                    break;
                case sounds.AudioCat.SoundeEffects:
                    i.soundSource.outputAudioMixerGroup = soundEffectGroup;
                    break;
            }

            if (i.onWake)
            {
                i.soundSource.Play();
            }

        }
    }
    public void playByName(string _audioName)
    {
        sounds audioPicked = null;

        foreach (sounds i in soundArray)
        {
            if (i.soundName == _audioName)
            {
                audioPicked = i;
                if (audioPicked != null)
                {
                    audioPicked.soundSource.Play();
                }
            }
        }
    }

    public void stopByName(string _audioName)
    {
        sounds audioPicked = null;

        foreach (sounds i in soundArray)
        {
            if (i.soundName == _audioName)
            {
                audioPicked = i;
                if (audioPicked != null)
                {
                    audioPicked.soundSource.Stop();
                }
            }
        }
    }

    public void mute()
    {
        if (AudioListener.volume == 1)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }

        PlayerPrefs.SetFloat(prefAudioMute, AudioListener.volume);
    }

    public void updateMixer()
    {
        musicGroup.audioMixer.SetFloat("Music Volume", Mathf.Log10(audioOptions.musicVolume) * 20);
        soundEffectGroup.audioMixer.SetFloat("Sound Effect Volume", Mathf.Log10(audioOptions.soundEffectVolume) * 20);
    }
}
