using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioGenerator : MonoBehaviour
{
    private float samplingFrequency = 48000;
    public float Freqency = 440f;
    public float Gain = 0.05f;
    private float Increment;
    private float Phase;

    private bool Initialized;
    public bool Running;

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
    
    private void AdjustIncrement(bool deviceChanged)
    {
        Increment = Freqency * 2f * Mathf.PI / samplingFrequency;
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
