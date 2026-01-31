using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject _glitchEffect;
    [SerializeField] bool _isTransition;
    [SerializeField] bool _isInMenuButNotMainMenu;
    [SerializeField] GameObject _mainMenuScreen;
    [SerializeField] GameObject _creditsScreen;
    [SerializeField] GameObject _infoScreen;
    void Start()
    {
        
    }

    void Update()
    {
        if (_isTransition)
        {
            StartCoroutine(TransitionScreenMenu());
        }

        if (_isInMenuButNotMainMenu)
        {
            if (Input.anyKey)
            {
                ShowMainMenu();
            }
        }
    }

    public IEnumerator TransitionScreenMenu()
    {

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
