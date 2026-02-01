using System.Collections;
using UnityEngine;

public class WallVFX : MonoBehaviour
{
    [SerializeField] Material _wallMat;
    [SerializeField] GameObject _glitchEffect;
    [SerializeField] ParticleSystem _happyMaskPart;
    [SerializeField] ParticleSystem _sadMaskPart;
    [SerializeField] ScreenShake _ss;

    [SerializeField] GameObject _neutralMask;
    [SerializeField] GameObject _happyMask;
    [SerializeField] GameObject _sadMask;


    public float minValue = 0f;
    public float maxValue = 1f;
    public float speed = 1f;

    static readonly int EmissionID = Shader.PropertyToID("_EmissionColor");

    public bool isRed = true;
    public bool maskChange = false;
    public bool isInDanger = false;


    void Update()
    {

        if (isRed)
        {
            float t = Mathf.PingPong(Time.time * speed, 1f);
            float intensity = Mathf.Lerp(minValue, maxValue, t);

            Color emissionColor = Color.red * intensity;

            _wallMat.SetColor(EmissionID, emissionColor);
            _wallMat.EnableKeyword("_EMISSION");
        }

        else
        {
            float t = Mathf.PingPong(Time.time * speed, 1f);
            float intensity = Mathf.Lerp(minValue, maxValue, t);

            Color emissionColor = Color.blue * intensity;

            _wallMat.SetColor(EmissionID, emissionColor);
            _wallMat.EnableKeyword("_EMISSION");
        }

        if (maskChange)
        {
            maskChange = false;
            StartCoroutine(ChangeMaskEffect());
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            maskChange = true;
        }

        if (isInDanger)
        {
            _ss.shake = true;
        }
        else { _ss.shake = false; }
    }

    private IEnumerator ChangeMaskEffect()
    {
        _glitchEffect.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        if (!isRed) { _happyMaskPart.Play();
            _neutralMask.SetActive(false);
            _happyMask.SetActive(true);
            _sadMask.SetActive(false);
        }
        else
        {
            _neutralMask.SetActive(false);
            _happyMask.SetActive(false);
            _sadMask.SetActive(true);
            _sadMaskPart.Play();
        }
        yield return new WaitForSeconds(0.2f);
        _glitchEffect.SetActive(false);
        isRed = !isRed;
       

    }
}
