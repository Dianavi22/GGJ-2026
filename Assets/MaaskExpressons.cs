using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaaskExpressons : MonoBehaviour
{
    [SerializeField] GameObject _chockedFace;
    [SerializeField] List<GameObject> _agroFace;
    public bool isAgro = false;
    void Start()
    {
    }

    void Update()
    {
        if (isAgro)
        {
            StartCoroutine(ISeeU());
            isAgro = false;
        }
    }

    public IEnumerator ISeeU()
    {
        _chockedFace.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        _chockedFace.SetActive(false);
        for (int i = 0; i < _agroFace.Count; i++)
        {
            _agroFace[i].SetActive(true);
        }

    }
}
