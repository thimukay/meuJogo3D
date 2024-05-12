using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using Cinemachine;

public class ShakeCamera : Singleton<ShakeCamera>
{
    public List<CinemachineVirtualCamera> virtualCamera;

    public float shakeTime;

    //public CinemachineBasicMultiChannelPerlin c;
    [Header("Shake Values")]
    public float amplitude = 3f;
    public float frequency = 3f;
    public float time = 3f;

    [NaughtyAttributes.Button]
    public void Shake()
    {
        Shake(amplitude, frequency, time);
    }

    public void Shake(float amplitude, float frequency, float time)
    {
        //c = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        for (int i = 0; i < virtualCamera.Count; i++)
        {
            virtualCamera[i].GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
            virtualCamera[i].GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;
        }
        

        shakeTime = time;
    }

    private void Update()
    {
        if(shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
        }
        else
        {
            for (int i = 0; i < virtualCamera.Count; i++)
            {
                virtualCamera[i].GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
                virtualCamera[i].GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0f;
            }
        }
    }
}
