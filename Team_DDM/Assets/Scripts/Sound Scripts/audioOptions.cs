using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    void Start()
    {
        revertMixer();
        audioManager.instance.updateMixer();
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

    public void saveMixer()
    {
        audioManager.instance.updateMixer();

        PlayerPrefs.SetFloat("prefMusicVolume", musicVolume);
        PlayerPrefs.SetFloat("prefSoundEffectVolume", soundEffectVolume);
    }

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

    public void revertMixer()
    {
        musicVolume = PlayerPrefs.GetFloat("prefMusicVolume");
        soundEffectVolume = PlayerPrefs.GetFloat("prefSoundEffectVolume");

        mSlider.value = musicVolume;
        seSlider.value = soundEffectVolume;

        musicVolumeText.text = ((int)(musicVolume * 100)).ToString();
        soundEffectVolumeText.text = ((int)(soundEffectVolume * 100)).ToString();
    }
}
