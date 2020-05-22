using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;


    public void FullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetQuality(int qualityIndex)
    {
        Debug.Log(qualityIndex);
        QualitySettings.SetQualityLevel(qualityIndex);  
    }


    public void SetVolume(float volume)
    {
//        Debug.Log(volume);
       audioMixer.SetFloat("volume", volume);
    }
}
