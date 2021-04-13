using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVoiceCommandable
{
    void RegisterVoiceCommands(IVoiceInputManager voiceInputManager);
}
