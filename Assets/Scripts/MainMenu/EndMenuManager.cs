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
    [SerializeField] GameObject  MaskAnim ;
    [SerializeField] GameObject  TextMenu;

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
        Invoke("ShowMask", 0.4f);
    }
    public void ShowMask()
    {
        StartCoroutine(TransitionScreenMenu());
        MaskAnim.SetActive(true);
        Invoke("ShowText", 1.2f);


    }
    public void ShowText()
    {
        StartCoroutine(TransitionScreenMenu());

        TextMenu.SetActive(true);
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
        StartCoroutine(MainMenu());
        //_cse.StartPlay();
        //Invoke("MainMenu", 2f);
    }

    public IEnumerator MainMenu()
    {
        StartCoroutine( TransitionScreenMenu());
        CloseScreenMenu();
        yield return new WaitForSeconds(0.6f);

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
