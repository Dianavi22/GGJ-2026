using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TextGlitch : MonoBehaviour
{
    private TMP_Text text;

     string normalText;

    [SerializeField] List<string> glitchText = new List<string>();

    public float minTimeBetweenGlitch = 1f;
    public float maxTimeBetweenGlitch = 4f;

    public float minGlitchDuration = 0.05f;
    public float maxGlitchDuration = 0.2f;

    void Start()
    {
        text = GetComponent<TMP_Text>();
        normalText = text.text;
        StartCoroutine(GlitchLoop());
    }

    IEnumerator GlitchLoop()
    {
        while (true)
        {
            float waitTime = Random.Range(minTimeBetweenGlitch, maxTimeBetweenGlitch);
            yield return new WaitForSeconds(waitTime);

            float glitchDuration = Random.Range(minGlitchDuration, maxGlitchDuration);
            text.text = glitchText[Random.Range(0, glitchText.Count)];

            yield return new WaitForSeconds(glitchDuration);

            text.text = normalText;
        }
    }
}