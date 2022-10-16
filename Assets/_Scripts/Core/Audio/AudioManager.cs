using System;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public AudioMixer masterMixer;

    public Sound[] sounds;

    [HideInInspector] public AudioSource musicTrack;

    private bool checkMusic;
    private bool checkSFX;

    private void Initialize()
    {
        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;

            s.source.outputAudioMixerGroup = s.mixer;
        }

        checkMusic = PlayerPrefs.GetInt("s_music") > 0 ? true : false;
        checkSFX = PlayerPrefs.GetInt("s_sound") > 0 ? true : false;
    }

    public void Play(string sound)
    {
        var s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.Play();
    }

    public void Stop(string sound)
    {
        var s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Stop();
    }

    public void AllStop()
    {
        foreach (var s in sounds)
            if (s.source != null)
                s.source.Stop();
    }

    public void ToggleMusic()
    {
        checkMusic = PlayerPrefs.GetInt("s_music") > 0 ? true : false;
        PlayerPrefs.SetInt("s_music", checkMusic ? 0 : 1);
        if (checkMusic)
            masterMixer.SetFloat("MusicVol", -5f);
        else
            masterMixer.SetFloat("MusicVol", -80f);
    }

    public void ToggleSFX()
    {
        checkSFX = PlayerPrefs.GetInt("s_sound") > 0 ? true : false;
        PlayerPrefs.SetInt("s_sound", checkSFX ? 0 : 1);
        if (checkSFX)
            masterMixer.SetFloat("SFXsVol", -12f);
        else
            masterMixer.SetFloat("SFXsVol", -80f);
    }

    #region Singleton Init

    private static AudioManager _instance;

    private void Awake() // Init in order
    {
        if (_instance == null)
            Init();
        else if (_instance != this)
            Destroy(gameObject);
    }

    public static AudioManager Instance // Init not in order
    {
        get
        {
            if (_instance == null)
                Init();
            return _instance;
        }
        private set => _instance = value;
    }

    private static void Init() // Init script
    {
        _instance = FindObjectOfType<AudioManager>();
        _instance.Initialize();
    }

    #endregion
}