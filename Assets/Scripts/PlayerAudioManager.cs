using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip[] sfx;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayWalk()
    {
        AudioClip sfx = this.sfx[0];
        audioSource.PlayOneShot(sfx, .5f);
    }

    public void PlaySwitch()
    {
        AudioClip sfx = this.sfx[1];
        audioSource.PlayOneShot(sfx, .5f);
    }
}
