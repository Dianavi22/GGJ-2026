using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Teagher.Rendering.PostProcessEffects;

public class CloseScreenEffect : MonoBehaviour
{
   [SerializeField] private List<GameObject> _deadPixel;
    [SerializeField] bool _isPlay;
    [SerializeField] float _interval;
    [SerializeField] float _dispProba;
    [SerializeField] float _dispIntensity;
    [SerializeField] float _colorProba;
    [SerializeField] float _colorIntensity;
    [SerializeField] float _dispStripSize;
    [SerializeField] MainMenuManager _mmm;
    [SerializeField] ParticleSystem _shutDownTVPart;
    public PostProcessVolume volume;
    private VideoGlitch videoGlitch;

    void Start()
    {
    }
    public void StartPlay()
    {
        StartCoroutine(ShowDeadPixel());
    }
    void Update()
    {
        if (!_isPlay) return;

        float speed = Time.deltaTime * 0.2f;

        videoGlitch.m_GlitchInterval.value += speed;
        videoGlitch.m_DispProbability.value += speed;
        videoGlitch.m_DispIntensity.value += speed;
        videoGlitch.m_ColorProbability.value += speed;
        videoGlitch.m_ColorIntensity.value += speed;
        videoGlitch.m_DispStripSize.value += speed;

        if (AllGlitchValuesReached())
        {
            _isPlay = false;
           
        }
    }

    public void Close()
    {
       
            _mmm.CloseScreenMenu();

       

    }

    bool AllGlitchValuesReached()
    {
       
        return
            videoGlitch.m_GlitchInterval.value >= _interval &&
            videoGlitch.m_DispProbability.value >= _dispProba &&
            videoGlitch.m_DispIntensity.value >= _dispIntensity &&
            videoGlitch.m_ColorProbability.value >= _colorProba &&
            videoGlitch.m_ColorIntensity.value >= _colorIntensity &&
            videoGlitch.m_DispStripSize.value >= _dispStripSize;
    }


    public IEnumerator ShowDeadPixel()
    {

        if (volume.profile.TryGetSettings(out videoGlitch))
        {
            videoGlitch.m_GlitchInterval.value = 0;
            videoGlitch.m_DispProbability.value = 0;
            videoGlitch.m_DispIntensity.value = 0;
            videoGlitch.m_ColorProbability.value = 0;
            videoGlitch.m_ColorIntensity.value = 0;
            videoGlitch.m_DispStripSize.value = 0;
            _isPlay = true;
        }
        for (int i = 0; i < _deadPixel.Count; i++)
        {
            _deadPixel[i].SetActive(true);
            yield return new WaitForSeconds(0.2f);

        }
        Close();
    }
}
