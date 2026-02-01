using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
using Teagher.Rendering.PostProcessEffects;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject _glitchEffect;
    [SerializeField] bool _isTransition;
    [SerializeField] bool _isInMenuButNotMainMenu;
    [SerializeField] GameObject _mainMenuScreen;
    [SerializeField] GameObject _creditsScreen;
    [SerializeField] GameObject _infoScreen;
    public CloseScreenEffect _cse;

    [SerializeField] AudioManager _audioManager;


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

    public void CloseScreenMenu(Action after)
    {
        if (volume.profile.TryGetSettings(out screenFading))
        {
            screenFading.blend.value = 0f;
            isFadingClose = true;

            after();
        }
    }

    public void Play()
    {
        Action after = () =>
        {
            StartCoroutine(LauchSceneAfterWait());
        };

        _cse.StartPlay(after);
    }

    void Update()
    {
        if (screenFading != null && isFadingClose)
        {
            screenFading.blend.value += Time.deltaTime * 2f;

            if (screenFading.blend.value <= 0f)
            {
                if (screenFading.blend.value >= 1f)
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

        if (_isTransition) { StartCoroutine(TransitionScreenMenu()); }

        if (_isInMenuButNotMainMenu)
        {
            if (_infoScreen.activeSelf)
            {
                if(Input.GetKeyUp(KeyCode.Escape) || Gamepad.current.bButton.wasPressedThisFrame) { ShowMainMenu();}
            }
            else
            {
                if(Input.anyKey || Gamepad.current.bButton.wasPressedThisFrame) { ShowMainMenu(); }
            }
        }
    }

    public IEnumerator LauchSceneAfterWait()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("KFMAP");
    }


    public IEnumerator TransitionScreenMenu()
    {
        _audioManager.PlayGlitchTransition();
        _glitchEffect.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        _glitchEffect.SetActive(false);
        _isTransition = false;
    }

    public void Quit()
    {
        Application.Quit();

    }

    public void ShowMainMenu()
    {
        _isTransition = true;
        _isInMenuButNotMainMenu = false;
        _mainMenuScreen.SetActive(true);
        _creditsScreen.SetActive(false);
        _infoScreen.SetActive(false);
    }



    public void ShowCredits()
    {
        _isTransition = true;
        _mainMenuScreen.SetActive(false);
        _creditsScreen.SetActive(true);
        _isInMenuButNotMainMenu = true;
    }

    public void ShowInfoMenu()
    {
        _isTransition = true;
        _mainMenuScreen.SetActive(false);
        _infoScreen.SetActive(true);
        _isInMenuButNotMainMenu = true;
    }

}
