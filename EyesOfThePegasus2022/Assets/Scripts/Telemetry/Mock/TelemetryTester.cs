using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelemetryTester : MonoBehaviour
{
    private ITelemetryRequester TelemetryRequester;
    public bool running;
    public AudioSource AudioSource;
    public AudioClip AudioClip;
    public void Init(TelemetryDistributor telemetryDistributor,
        TelemetrySonification telemetrySonification, ITelemetryRequester telemetryRequester)
    {
        telemetryDistributor.SubscribeNewTelemetryDataListener(
            ((sender, data) =>
            {
                telemetrySonification.SetCurrentValue(data.heart_bpm);
            }));
        telemetryDistributor.SubscribeNewTelemetryDataListener(((sender, data) =>
        {
            Debug.Log(data);
        }));
        telemetrySonification.TurnOn();

        TelemetryRequester = telemetryRequester;
    }

    [ContextMenu("Toggle polling")]
    private void ToggleTelemetry()
    {
        if (!running)
        {
            TelemetryRequester.StartPolling();
        }
        else
        {
            TelemetryRequester.StopPolling();
        }

        running = !running;
    }
}
