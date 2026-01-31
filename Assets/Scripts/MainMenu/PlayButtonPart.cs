using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonPart : MonoBehaviour
{
    [SerializeField] ParticleSystem _partButton1;
    [SerializeField] ParticleSystem _partButton2;

    void Start()
    {
        _partButton1.gameObject.SetActive(true);
        _partButton2.gameObject.SetActive(true);
        DisablePartsButton();

    }

    public void ActivePartsButton()
    {
        _partButton2.GetComponent<ParticleSystem>().GetComponent<Renderer>().enabled = true;
        _partButton1.GetComponent<ParticleSystem>().GetComponent<Renderer>().enabled = true ;
    }

    public void DisablePartsButton()
    {
        _partButton2.GetComponent<ParticleSystem>().GetComponent<Renderer>().enabled = false;
        _partButton1.GetComponent<ParticleSystem>().GetComponent<Renderer>().enabled = false;
    }
}
