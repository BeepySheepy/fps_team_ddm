using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.UI;

public class audioOptions : MonoBehaviour
{
    public static float musicVolume { get; private set; }
    public static float soundEffectVolume { get; private set; }


    public Slider mSlider;
    public Slider seSlider;
    [SerializeField] TextMeshProUGUI musicVolumeText;
    [SerializeField] TextMeshProUGUI soundEffectVolumeText;
    [SerializeField] GameObject audioSavedImage;
    [SerializeField] GameObject audioClearedImage;

    void Start()
    {
        
        #region Problem B
        //if (PlayerPrefs.HasKey("prefMusicVolume") || PlayerPrefs.HasKey("prefSoundEffectVolume"))
        //{
        //    Debug.Log("Player prefs saved1");
        //    revertMixer();
        //    audioManager.instance.updateMixer();
        //}
        //else
        //{
        //    Debug.Log("Player Pref saved2");
        //    Default();
        //}
        #endregion
    }
    public void musicSlider(float _volume)
    {
        musicVolume = _volume;
        musicVolumeText.text = ((int)(_volume * 100)).ToString();
    }
    public void soundEffectSlider(float _volume)
    {
        soundEffectVolume = _volume;
        soundEffectVolumeText.text = ((int)(_volume * 100)).ToString();
    }

    #region Sound Test Functions
    public void musicTest()
    {
        StartCoroutine(testAudio1());
    }
    public void soundEffectTest()
    {
        StartCoroutine(testAudio2());
    }

    IEnumerator testAudio1()
    {
        audioManager.instance.updateMixer();
        yield return new WaitForSeconds(3f);
        revertMixer();
        audioManager.instance.updateMixer();
    }
    IEnumerator testAudio2()
    {
        audioManager.instance.updateMixer();
        audioManager.instance.playByName("Button Click");
        yield return new WaitForSeconds(2f);
        revertMixer();
        audioManager.instance.updateMixer();
    }
    #endregion

    public void revertMixer()
    {
        #region Problem A
        //if (!PlayerPrefs.HasKey("prefMusicVolume") && !PlayerPrefs.HasKey("prefSoundEffectVolume"))
        //{
        //    musicVolume = 1;
        //    soundEffectVolume = 1;
        //}
        //else
        //{
        //    musicVolume = PlayerPrefs.GetFloat("prefMusicVolume");
        //    soundEffectVolume = PlayerPrefs.GetFloat("prefSoundEffectVolume");
        //}
        #endregion

        musicVolume = PlayerPrefs.GetFloat("prefMusicVolume");
        soundEffectVolume = PlayerPrefs.GetFloat("prefSoundEffectVolume");
        mSlider.value = musicVolume;
        seSlider.value = soundEffectVolume;

        musicVolumeText.text = ((int)(musicVolume * 100)).ToString();
        soundEffectVolumeText.text = ((int)(soundEffectVolume * 100)).ToString();
    }
    public void saveMixer()
    {
        audioManager.instance.updateMixer();

        PlayerPrefs.SetFloat("prefMusicVolume", musicVolume);
        PlayerPrefs.SetFloat("prefSoundEffectVolume", soundEffectVolume);
        mSlider.value = musicVolume;
        seSlider.value = soundEffectVolume;

        musicVolumeText.text = ((int)(musicVolume * 100)).ToString();
        soundEffectVolumeText.text = ((int)(soundEffectVolume * 100)).ToString();

        StartCoroutine(audioSaved());
    }

    IEnumerator audioSaved()
    {
        audioSavedImage.SetActive(true);
        yield return new WaitForSeconds(1f);
        audioSavedImage.SetActive(false);
    }

    public void deleteData()
    {
        PlayerPrefs.DeleteAll();
        Default();
        StartCoroutine(audioCleared());
    }
    IEnumerator audioCleared()
    {
        audioClearedImage.SetActive(true);
        yield return new WaitForSeconds(1f);
        audioClearedImage.SetActive(false);
    }

    void Default()
    {
        musicVolume = 1;
        soundEffectVolume = 1;

        mSlider.value = musicVolume;
        seSlider.value = soundEffectVolume;

        musicVolumeText.text = ((int)(musicVolume * 100)).ToString();
        soundEffectVolumeText.text = ((int)(soundEffectVolume * 100)).ToString();

        audioManager.instance.updateMixer();
    }
}
