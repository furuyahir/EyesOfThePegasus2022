using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WIM : MonoBehaviour, IVoiceCommandable, IOpUI
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

    public void Remove()
    {
        Destroy(gameObject);
    }

    public void TurnOn()
    {
        gameObject.SetActive(true);
    }

    public void TurnOff()
    {
        gameObject.SetActive(false);
    }

    public bool IsOn()
    {
        return gameObject.activeInHierarchy;
    }
}
