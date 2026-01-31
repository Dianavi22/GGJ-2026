using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonPart : MonoBehaviour
{
    [SerializeField] ParticleSystem _partButton1;
    [SerializeField] ParticleSystem _partButton2;
    void Start()
    {
        _partButton1.gameObject.SetActive(true);
        _partButton2.gameObject.SetActive(true);
    }

    void Update()
    {
        
    }
}
