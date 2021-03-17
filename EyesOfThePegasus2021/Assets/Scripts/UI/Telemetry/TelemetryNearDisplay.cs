using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class TelemetryNearDisplay : MonoBehaviour, IOpUI
{
    public FollowMeToggle FollowMeToggle;

    public void RegisterVoiceCommands(VoiceInputManager voiceInputManager)
    {
        voiceInputManager.AddInputCommand(InputAction.Create("Show Telemetry",
            "Show Telemetry UI", () =>
            {
                TurnOn();
            }));
        voiceInputManager.AddInputCommand(InputAction.Create("Hide Telemetry",
            "Hide Telemetry UI", () =>
            {
                TurnOff();
            }));
        voiceInputManager.AddInputCommand(InputAction.Create("Follow Telemetry",
            "Turn on Telemetry UI following behavior", () =>
            {
                FollowMeToggle.ToggleFollowMeBehavior(true);
            }));
        voiceInputManager.AddInputCommand(InputAction.Create("Pin Telemetry",
            "Turn on Telemetry UI following behavior", () =>
            {
                FollowMeToggle.ToggleFollowMeBehavior(false);
            }));
    }

    public void Init()
    {
        FollowMeToggle = GetComponent<FollowMeToggle>();
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
