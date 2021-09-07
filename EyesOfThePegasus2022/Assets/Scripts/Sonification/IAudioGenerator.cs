using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAudioGenerator
{
    void Init();
    void SetRunning(bool run);
    void SetFreqency(float frequency);
    bool Running { get; }
    void Destroy();
}
