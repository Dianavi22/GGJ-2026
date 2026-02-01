using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaaskExpressons : MonoBehaviour
{
    [SerializeField] GameObject _chockedFace;
    [SerializeField] List<GameObject> _agroFace;
    public bool isAgro = false;

    public bool isRotating = false;
    [SerializeField] float rotationSpeed = 90f;
    private Quaternion initialRotation;
    void Start()
    {
        initialRotation = transform.rotation;
    }

    void Update()
    {
        if (isAgro)
        {
            StartCoroutine(ISeeU());
            isAgro = false;
        }
        if (!isRotating) return;

        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime, Space.Self);
    }

    public void ResetRotation()
    {
        transform.rotation = initialRotation;
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

    public void ResetFace()
    {
        _chockedFace.SetActive(false);

        for (int i = 0; i < _agroFace.Count; i++)
        {
            _agroFace[i].SetActive(false);
        }

    }
}
