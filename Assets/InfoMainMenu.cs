using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoMainMenu : MonoBehaviour
{
    [SerializeField] List<GameObject> _lights;
    [SerializeField] Material _happyMat;
    [SerializeField] Material _sadMat;
    [SerializeField] GameObject _glitch;
    [SerializeField] GameObject _comedyText;
    [SerializeField] GameObject _tragedyText;

    private bool isRed = true;
    void Start()
    {
        ChangeMode();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Glitch());
            ChangeMode();
        }
        
    }

    private void ChangeMode()
    {
        if (!isRed)
        {
            for (int i = 0; i < _lights.Count; i++)
            {
               
                    _lights[i].GetComponent<Renderer>().material = _sadMat;
                 
            }
            

            _comedyText.SetActive(true);
            _tragedyText.SetActive(false);
        }
        else
        {
            for (int i = 0; i < _lights.Count; i++)
            {

                _lights[i].GetComponent<Renderer>().material = _happyMat;

            }
            _comedyText.SetActive(false);
            _tragedyText.SetActive(true);
        }
        isRed = !isRed;
    }

    private IEnumerator Glitch()
    {
        _glitch.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        _glitch.SetActive(false);

    }
}
