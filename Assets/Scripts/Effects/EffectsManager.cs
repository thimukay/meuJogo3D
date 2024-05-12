using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Ebac.Core.Singleton;

public class EffectsManager : Singleton<EffectsManager>
{
    public PostProcessVolume processVolume;
    [SerializeField] private Vignette _vignette;

    public float duration = .1f;
    private float _vignetteIntensity = 0.5f;
    private float _vignetteIntensityPlus = .03f;

    [NaughtyAttributes.Button]
    public void ChangeVignette()
    {
        StartCoroutine(FlashColorVignette());
    }
    public void ResetVignette()
    {
        _vignette.intensity.Override(0);
        _vignetteIntensity = 0.5f;
    }

    IEnumerator FlashColorVignette()
    {
        Vignette tmp;


        if (processVolume.profile.TryGetSettings<Vignette>(out tmp))
        {
            _vignette = tmp;
        }

        ColorParameter c = new ColorParameter();
        FloatParameter f = new FloatParameter();

        float time = 0;
        while (time < duration)
        {
            //c.value = Color.Lerp(Color.black, Color.red, time/duration);
            f.value = Mathf.Lerp(_vignette.intensity.value, _vignetteIntensity, time / duration);
            c.value = Color.red;
            time += Time.deltaTime;
            _vignette.intensity.Override(f);
            _vignette.color.Override(c);
            yield return new WaitForEndOfFrame();
        }

        _vignetteIntensity += _vignetteIntensityPlus;
        /*time = 0;
        while (time < duration)
        {
            //c.value = Color.Lerp(Color.red, Color.black, time / duration);
            c.value = Color.red;
            time += Time.deltaTime;
            _vignette.intensity.Override(life % 10);
            _vignette.color.Override(c);
            yield return new WaitForEndOfFrame();
        }*/

    }
}
