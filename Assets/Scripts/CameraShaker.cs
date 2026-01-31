using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private float idleAmplitude;
    [SerializeField] private float idleFrequency;

    [SerializeField] private float walkAmplitude;

    [SerializeField] private float walkFrequency;
    [SerializeField] private float sprintAmplitude;
    [SerializeField] private float sprintFrequency;

    [SerializeField] private float transitionMultiplier;

    [SerializeField] private CinemachineBasicMultiChannelPerlin noise;

    public void ShakeCamera(string type = "Idle")
    {
        switch (type)
        {
            case "Walk":
            SetShake(walkAmplitude,walkFrequency);
            break;
            case "Idle":
            SetShake(idleAmplitude, idleFrequency);
            break;
            case "Sprint":
            SetShake(sprintAmplitude, sprintFrequency);
            break;
        }
    }
    private void SetShake(float amplitude, float frequency)
    {
        float oldAmplitude = noise.AmplitudeGain;
        float oldFrequency = noise.FrequencyGain;
        noise.AmplitudeGain = Mathf.Lerp(oldAmplitude, amplitude, Time.deltaTime * transitionMultiplier);
        noise.FrequencyGain = Mathf.Lerp(oldFrequency, frequency, Time.deltaTime* transitionMultiplier);
    }

}
