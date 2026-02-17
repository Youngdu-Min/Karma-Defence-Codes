using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerManager : MonoBehaviour
{
    public AudioMixer masterMixer;
    float volume_BGM;
    float volume_SFX;
    
    public static AudioMixerManager instance = null;
    public static AudioMixerManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        
        if (instance != null && instance != this) 
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        masterMixer.SetFloat("SFX", PlayerPrefs.GetFloat("VolBGM"));
        masterMixer.SetFloat("BGM", PlayerPrefs.GetFloat("VolSFX")); 

    }

    public void SetBgmLv(float bgmLv)
    {
        volume_BGM = Mathf.Log10(bgmLv) * 20;
        masterMixer.SetFloat("BGM", volume_BGM);
    }
    public void SetSfxLv(float sfxLv)
    {
        volume_SFX = Mathf.Log10(sfxLv) * 20;
        masterMixer.SetFloat("SFX", volume_SFX);
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("VolBGM", volume_BGM);
        PlayerPrefs.SetFloat("VolSFX", volume_SFX);
    }
}
