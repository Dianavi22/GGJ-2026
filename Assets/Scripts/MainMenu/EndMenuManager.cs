using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
using Teagher.Rendering.PostProcessEffects;
using UnityEngine.SceneManagement;

public class EndMenuManager : MonoBehaviour
{
    [SerializeField] GameObject _glitchEffect;
    public CloseScreenEffect _cse;


    public PostProcessVolume volume;
    private ScreenFading screenFading;
    [SerializeField] private bool isFadingOpen = false;
    [SerializeField] private bool isFadingClose = false;

    void Start()
    {
        OpenScreenMenu();
    }

    public void OpenScreenMenu()
    {
        if (volume.profile.TryGetSettings(out screenFading))
        {
            screenFading.blend.value = 1f;
            isFadingOpen = true;
        }
    }

    public void CloseScreenMenu()
    {
        if (volume.profile.TryGetSettings(out screenFading))
        {
            screenFading.blend.value = 0f;
            isFadingClose = true;
        }
    }

    public void Menu()
    {
        _cse.StartPlay();
        SceneManager.LoadScene("SampleScene");
    }

    void Update()
    {
        if (screenFading != null && isFadingClose)
        {
            screenFading.blend.value += Time.deltaTime * 2f;

            if (screenFading.blend.value <= 0f)
            {
              if(  screenFading.blend.value >= 1f)
                isFadingClose = false;
            }
        }

        if (screenFading != null && isFadingOpen)
        {
            screenFading.blend.value -= Time.deltaTime * 2f;

            if (screenFading.blend.value <= 0f)
            {
                screenFading.blend.value = 0f;
                isFadingOpen = false;
            }
        }
    }


    public IEnumerator TransitionScreenMenu()
    {

        _glitchEffect.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        _glitchEffect.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();

    }



}
