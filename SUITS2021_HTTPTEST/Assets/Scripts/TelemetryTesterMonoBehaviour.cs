using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelemetryTesterMonoBehaviour : MonoBehaviour
{
    private TelemetryRequester TelemetryRequester;
    // Start is called before the first frame update
    void Start()
    {
        TelemetryRequester = new TelemetryRequester();
        TelemetryRequester.SubscribeTelemetryReceivedListener(PrintTelemetryData);
    }

    [ContextMenu("Start")]
    public void StartTelemetry()
    {
        TelemetryRequester.StartPolling();
    }

    [ContextMenu("Stop")]
    public void StopTelemetry()
    {
        TelemetryRequester.StopPolling();
    }

    private void PrintTelemetryData(object sender, TelemetryData data)
    {
        Debug.Log(data);
    }
    
}
