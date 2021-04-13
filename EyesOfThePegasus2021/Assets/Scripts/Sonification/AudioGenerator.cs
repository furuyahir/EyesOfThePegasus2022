using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioGenerator : MonoBehaviour, IAudioGenerator
{
    private float samplingFrequency = 48000;
    public float Frequency = 440f;
    public float Gain = 0.05f;
    private float Increment;
    private float Phase;

    private bool Initialized;
    public bool Running { get; private set; }
    public bool Interpolate;
    public float SecondsToTween = 1;
    
    private float Frequency1;
    private float Frequency2;
    private float tweenTime;
    
    [ContextMenu("Init")]
    public void Init()
    {
        samplingFrequency = AudioSettings.outputSampleRate;
        AudioSettings.OnAudioConfigurationChanged += AdjustIncrement;
        AdjustIncrement(false);
        Initialized = true;
    }

    public void SetRunning(bool run)
    {
        Running = run;
    }

    public void SetFreqency(float freqency)
    {
        Frequency1 = Frequency2;
        Frequency2 = freqency;
        tweenTime = 0;
    }

    private void Update()
    {
        if (!Running)
        {
            return;
        }
        tweenTime += Time.deltaTime;
        Frequency = Mathf.Lerp(Frequency1, Frequency2, tweenTime);
        AdjustIncrement(false);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void AdjustIncrement(bool deviceChanged)
    {
        samplingFrequency = AudioSettings.outputSampleRate;
        Increment = Frequency * 2f * Mathf.PI / samplingFrequency;
    }
    
    private void OnAudioFilterRead(float[] data, int channels)
    {
        if (!Initialized | !Running)
        {
            return;
        }

        for (int i = 0; i < data.Length; i++)
        {
            Phase += Increment;
            if (Phase > 2f * Mathf.PI)
            {
                Phase = 0;
            }
            data[i] = Gain * Mathf.Sin(Phase);
            
            if (channels != 2)
            {
                continue;
            }
            data[i + 1] = data[i];
            i++;
        }
    }
}
