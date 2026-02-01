using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource sfxAudioSource;
    [SerializeField] AudioClip mainTheme;
    [SerializeField] AudioClip[] sfx;

    AudioSource mainAudioSource;

    private void Awake()
    {
        mainAudioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayMainTheme();
    }

    void PlayMainTheme()
    {
        mainAudioSource.clip = mainTheme;
        mainAudioSource.Play();
    }

    public void PlayGlitch()
    {

    }
}
