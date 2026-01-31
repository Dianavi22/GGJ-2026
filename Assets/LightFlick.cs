using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlick : MonoBehaviour
{

    [SerializeField] Light _light;

    [SerializeField] float minTimeBetweenGlitch = 1f;
    [SerializeField] float maxTimeBetweenGlitch = 4f;
   float _startLight;

    [SerializeField] float minGlitchDuration = 0.05f;
    [SerializeField] float maxGlitchDuration = 0.2f;
    void Start()
    {
        _startLight = _light.intensity;
        StartCoroutine(GlitchLoop());

    }

    void Update()
    {
        StartCoroutine(GlitchLoop());

    }

    IEnumerator GlitchLoop()
    {
        while (true)
        {
            float waitTime = Random.Range(minTimeBetweenGlitch, maxTimeBetweenGlitch);
            yield return new WaitForSeconds(waitTime);

            float glitchDuration = Random.Range(minGlitchDuration, maxGlitchDuration);
            _light.intensity = Random.Range(20, 25);

            yield return new WaitForSeconds(glitchDuration);

            _light.intensity = _startLight;
        }
       
    }
}
