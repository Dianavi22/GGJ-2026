using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        if (openSfx != null)
        {
            AudioSource mainSfxAudioSource = gameObject.AddComponent<AudioSource>();
            mainSfxAudioSource.playOnAwake = false;
            mainSfxAudioSource.clip = openSfx;
            mainSfxAudioSource.Play();
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = mainTheme;
        audioSource.loop = true;
        audioSource.PlayDelayed(openSfx != null ? openSfx.length - 2 : 0);
    }

    public void PlayButtonSelect()
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

    public void PlayTVOffTransition()
    {
        AudioClip glitchSfx = sfx[4];
        audioSource.PlayOneShot(glitchSfx, 1f);
    }
}
