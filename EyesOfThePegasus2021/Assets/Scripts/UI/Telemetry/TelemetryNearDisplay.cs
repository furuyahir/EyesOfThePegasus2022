using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelemetryNearDisplay : MonoBehaviour, IOpUI
{
    public void RegisterVoiceCommands(VoiceInputManager voiceInputManager)
    {
        voiceInputManager.AddInputCommand(InputAction.Create("Show Telemetry", KeyCode.T,
            "Show Telemetry UI", () =>
            {
                TurnOn();
            }));
        voiceInputManager.AddInputCommand(InputAction.Create("Hide Telemetry", KeyCode.R,
            "Hide Telemetry UI", () =>
            {
                TurnOff();
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
