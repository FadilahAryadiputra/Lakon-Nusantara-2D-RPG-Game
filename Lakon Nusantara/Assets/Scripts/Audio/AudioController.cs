using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioSource hitSource;
    [SerializeField] List<AudioClip> hitClip = new List<AudioClip>();
    [SerializeField] AudioSource buttonSource;
    [SerializeField] List<AudioClip> buttonClip = new List<AudioClip>();

    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "SfxVolume";

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadVolume();
    }

    // void nextUpdate()
    // {
    //     if(SceneManager.GetActiveScene().name == "CutScene")
    //     {
    //         AudioController.instance.GetComponent<AudioSource>().Pause();
    //     }
    //     else
    //     {
    //         AudioManager.instance.GetComponent<AudioSource>().UnPause();
    //     }
    // }
    
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "TestingKotaJateng" || SceneManager.GetActiveScene().name == "CutScene")
        {
            AudioController.instance.GetComponent<AudioSource>().Pause();
        }
        else
        {
            AudioController.instance.GetComponent<AudioSource>().UnPause();
        }
    }

    public void hitSFX()
    {
        AudioClip clip = hitClip[Random.Range(0, hitClip.Count)];
        hitSource.PlayOneShot(clip);
    }

    public void buttonSFX()
    {
        AudioClip clip = buttonClip[Random.Range(0, buttonClip.Count)];
        buttonSource.PlayOneShot(clip);
    }

    void LoadVolume() //Volume saved in VolumeSetting.cs
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float SfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        mixer.SetFloat(VolumeSetting.Mixer_Music, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(VolumeSetting.Mixer_Sfx, Mathf.Log10(SfxVolume) * 20);
    }
}
