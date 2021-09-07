using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelemetrySonification : IOpUI
{
    private IAudioGenerator AudioGenerator;
    private float BaseFreqency, MinFreqency, MaxFreqency;
    private float BaseValue, MinValue, MaxValue;
    private float CurrentValue;
    public bool Running;

    public TelemetrySonification(IAudioGenerator audioGenerator, float baseFreqency, float baseValue, float 
    minFreqency, 
    float maxFreqency, 
        float minValue, float maxValue)
    {

        AudioGenerator = audioGenerator;
        BaseFreqency = baseFreqency;
        MinFreqency = minFreqency;
        MaxFreqency = maxFreqency;
        BaseValue = baseValue;
        MinValue = minValue;
        MaxValue = maxValue;
    }

    public void SetBaseValue(float baseValue)
    {
        BaseValue = baseValue;
    }

    public void SetBaseFrequency(float baseFrequency)
    {
        BaseFreqency = baseFrequency;
    }

    public void SetCurrentValue(float value)
    {
        CurrentValue = value;
        AudioGenerator.SetFreqency(CalculateFreqency(CurrentValue));
    }

    public void Remove()
    {
        AudioGenerator.Destroy();
    }

    public void TurnOn()
    {
        AudioGenerator.SetRunning(true);
        Running = true;
    }

    public void TurnOff()
    {
        AudioGenerator.SetRunning(false);
        Running = false;
    }

    public bool IsOn()
    {
        return Running;
    }

    private float CalculateFreqency(float value)
    {
        float percentageValue = (value - MinValue) / (MaxValue - MinValue);
        float x = percentageValue * 12 - 6;
        float fx = 1 / (float)(1 + Math.Pow(Math.E, x * -1));
        return fx * (MaxFreqency - MinFreqency) + MinFreqency;
    }
}
