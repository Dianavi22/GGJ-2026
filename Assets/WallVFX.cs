using System.Collections;
using UnityEngine;

public class WallVFX : MonoBehaviour
{
    [SerializeField] Material _wallMat;
    [SerializeField] GameObject _glitchEffect;
    [SerializeField] ParticleSystem _happyMaskPart;
    [SerializeField] ParticleSystem _sadMaskPart;
    [SerializeField] ScreenShake _ss;
    [SerializeField] PlayerController _playerMask;

    public float minValue = 0f;
    public float maxValue = 1f;
    public float speed = 1f;

    static readonly int EmissionID = Shader.PropertyToID("_EmissionColor");

    public bool isNeutral = true;
    public bool isRed = false;
    public bool isBlue = false;
    public bool isInDanger = false;

    private PlayerController.Masks _currentActiveMask;

    private void Start()
    {
        _currentActiveMask = _playerMask.ActiveMask;
    }
    void Update()
    {
        if (_playerMask.ActiveMask == PlayerController.Masks.NEUTRAL)
        {
            float t = Mathf.PingPong(Time.time * speed, 1f);
            float intensity = Mathf.Lerp(minValue, maxValue, t);

            Color emissionColor = Color.white * intensity;

            _wallMat.SetColor(EmissionID, emissionColor);
            _wallMat.EnableKeyword("_EMISSION");
        }


        else if (_playerMask.ActiveMask == PlayerController.Masks.RED)
        {
            float t = Mathf.PingPong(Time.time * speed, 1f);
            float intensity = Mathf.Lerp(minValue, maxValue, t);

            Color emissionColor = Color.red * intensity;

            _wallMat.SetColor(EmissionID, emissionColor);
            _wallMat.EnableKeyword("_EMISSION");
        }

        else if (_playerMask.ActiveMask == PlayerController.Masks.BLUE)
        {
            float t = Mathf.PingPong(Time.time * speed, 1f);
            float intensity = Mathf.Lerp(minValue, maxValue, t);

            Color emissionColor = Color.blue * intensity;

            _wallMat.SetColor(EmissionID, emissionColor);
            _wallMat.EnableKeyword("_EMISSION");
        }

        if (_currentActiveMask != _playerMask.ActiveMask)
        {
            _currentActiveMask = _playerMask.ActiveMask;
            StopCoroutine(ChangeMaskEffect());
            StartCoroutine(ChangeMaskEffect());
        }

        if (isInDanger)
        {
            _ss.shake = true;
        }
        else
        {
            _ss.shake = false;
        }
    }

    private IEnumerator ChangeMaskEffect()
    {
        _glitchEffect.SetActive(true);

        if (isRed) _sadMaskPart.Play();

        else if(isBlue) _sadMaskPart.Play();
        
        yield return new WaitForSeconds(0.2f);
        _glitchEffect.SetActive(false);
        isRed = !isRed;

    }
}
