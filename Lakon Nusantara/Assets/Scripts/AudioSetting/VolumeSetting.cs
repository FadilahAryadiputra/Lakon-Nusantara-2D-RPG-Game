using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSetting : MonoBehaviour
{
   [SerializeField] AudioMixer mixer;
   [SerializeField] Slider musicSlider;
   [SerializeField] Slider sfxSlider;

   public const string Mixer_Music = "MusicVolume";
   public const string Mixer_Sfx = "sfxVolume";

   void Awake()
   {
    musicSlider.onValueChanged.AddListener(SetMusicVolume);
    sfxSlider.onValueChanged.AddListener(SetSFXVolume);
   }

   void OnDisable()
   {
    PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, musicSlider.value);
    PlayerPrefs.SetFloat(AudioManager.SFX_KEY, sfxSlider.value);
   }

   void Start()
   {
    musicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 1f);
    sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 1f);
   }


   void SetMusicVolume(float value)
   {
    mixer.SetFloat(Mixer_Music, Mathf.Log10(value) * 20);
   }
   void SetSFXVolume(float value)
   {
    mixer.SetFloat(Mixer_Sfx, Mathf.Log10(value) * 20);
   }
}
