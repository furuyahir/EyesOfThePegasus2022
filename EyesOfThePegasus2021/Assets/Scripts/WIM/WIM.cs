using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WIM : MonoBehaviour, IVoiceCommandable
{
    public void RegisterVoiceCommands(IVoiceInputManager voiceInputManager)
    {
        voiceInputManager.AddVoiceCommand(InputAction.Create("Show wim", "Show the WIM", () =>
        {
            gameObject.SetActive(true);
        }));
        
        voiceInputManager.AddVoiceCommand(InputAction.Create("Hide wim", "Hide the WIM", () =>
        {
            gameObject.SetActive(false);
        }));
    }
}
