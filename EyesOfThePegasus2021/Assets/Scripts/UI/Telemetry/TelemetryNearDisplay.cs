using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;

public class TelemetryNearDisplay : MonoBehaviour, IOpUI, IVoiceCommandable
{
    public FollowMeToggle FollowMeToggle;
    public GameObject BackPlate;
    public GameObject ButtonParent;
    public GameObject ButtonToClone;
    public GridObjectCollection ButtonCollection;

    public void RegisterVoiceCommands(IVoiceInputManager voiceInputManager)
    {
        voiceInputManager.AddVoiceCommand(InputAction.Create("Show Telemetry",
            "Show Telemetry UI", () =>
            {
                TurnOn();
            }));
        voiceInputManager.AddVoiceCommand(InputAction.Create("Hide Telemetry",
            "Hide Telemetry UI", () =>
            {
                TurnOff();
            }));
        voiceInputManager.AddVoiceCommand(InputAction.Create("Follow Telemetry",
            "Turn on Telemetry UI following behavior", () =>
            {
                FollowMeToggle.ToggleFollowMeBehavior(true);
            }));
        voiceInputManager.AddVoiceCommand(InputAction.Create("Pin Telemetry",
            "Turn on Telemetry UI following behavior", () =>
            {
                FollowMeToggle.ToggleFollowMeBehavior(false);
            }));
    }

    public void Init(TelemetryDistributor telemetryDistributor)
    {
        FollowMeToggle = GetComponent<FollowMeToggle>();
        telemetryDistributor.SubscribeNewTelemetryDataListener(OnNewTelemetryData);
    }

    private void ResizeBackplate()
    {
        float width = 0.032f * (ButtonCollection.Columns + 1);
        float height = 0.032f * (ButtonCollection.Rows + 1);
        float depth = BackPlate.transform.localScale.z;
        BackPlate.transform.localScale = new Vector3(width, height, depth);
    }

    private void OnNewTelemetryData(object sender, TelemetryData data)
    {

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
