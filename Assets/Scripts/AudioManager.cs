using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] AudioClip mainTheme;
    [SerializeField] AudioClip[] sfx;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;   
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayMainTheme();
    }

    void PlayMainTheme()
    {

        AudioClip openSfx = sfx[0];
        AudioSource mainSfxAudioSource = gameObject.AddComponent<AudioSource>();
        mainSfxAudioSource.playOnAwake = false;
        mainSfxAudioSource.clip = openSfx;
        mainSfxAudioSource.Play();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = mainTheme;
        audioSource.loop = true;
        audioSource.PlayDelayed(openSfx.length - 2);
    }

    public void ButtonSelect()
    {
        AudioClip buttonSfx = sfx[1];
        audioSource.PlayOneShot(buttonSfx,.4f);
    }

    public void PlayGlitchTransition()
    {
        AudioClip glitchSfx = sfx[2];
        audioSource.PlayOneShot(glitchSfx, .05f);
    }

    public void PlayMegaGlitchTransition()
    {
        audioSource.Stop();

        AudioClip glitchSfx = sfx[3];
        audioSource.PlayOneShot(glitchSfx,1f);
    }
}
