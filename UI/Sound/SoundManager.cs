using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    private AudioSource bgmSource;
    private AudioSource effectSource;
    private AudioSource loopEffectSource;
    public AudioClip click;
    public AudioClip win;
    public AudioClip defeat;
    public AudioClip reinforce;
    public AudioClip skill1;
    public AudioClip skill2;
    public AudioClip skill3;
    public AudioClip unitAtt1;
    public AudioClip unitAtt2;
    public AudioClip unitAtt3;
    public AudioClip damaged;
    public AudioClip bossAppear;
    public AudioClip bossAttack;
    public AudioClip bossDie;


    public AudioMixerGroup bgmOutput;

    [SerializeField] private AudioClip[] bgmClip;

    // public int audioNumber;

    public static SoundManager instance = null;
    public static SoundManager Instance
    {
        get { return instance; }
    }


    private void Awake()
    {
        if(instance != null && instance != this)    // bgm매니저는 하나만 있어야함
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);


        GameObject child = new GameObject("BGM");
        child.transform.SetParent(transform);
        bgmSource = child.AddComponent<AudioSource>();
        bgmSource.loop = true;
        bgmSource.outputAudioMixerGroup = bgmOutput;
        

        effectSource = GetComponent<AudioSource>();
        effectSource.loop = false;

        loopEffectSource = GetComponent<AudioSource>();

        /*
        for (int i = 0; i < effectSource.Length; i++)
        {
            effectSource[i] = GetComponent<AudioSource>();
            effectSource[i].loop = false;
        }
        */
    }
    void Start()
    {
        ChangeClip(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnLevelWasLoaded(int level)
    {
        ChangeClip(level);
        
    }

    public void ChangeClip(int num)
    {
        if (bgmSource.isPlaying) bgmSource.Stop();
        // audioNumber = num;
        bgmSource.clip = bgmClip[num];
        bgmSource.Play();

    }

    public void StopClip()
    {
        bgmSource.Stop();
    }

    public void PlaySE(AudioClip clip)
    {
        // effectSource.Stop();
        // if (effectSource[i].isPlaying) return;
        // effectSource[i].clip = clip;
        effectSource.PlayOneShot(clip);
    }
    public void PlayLoopSE(AudioClip clip)
    {
        // effectSource.Stop();
        // if (effectSource[i].isPlaying) return;
        // effectSource[i].clip = clip;

        loopEffectSource.loop = true;
        loopEffectSource.clip = clip;
        loopEffectSource.Play();
        // loopEffectSource.PlayOneShot(clip);
    }

    public void CancelLoop()
    {
        loopEffectSource.loop = false;
    }
}
