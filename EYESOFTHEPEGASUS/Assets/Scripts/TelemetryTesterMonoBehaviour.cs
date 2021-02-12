using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelemetryTesterMonoBehaviour : MonoBehaviour
{
    private TelemetryDistributor TelemetryDistributor;
    private TelemetryRequester TelemetryRequester;
    // Start is called before the first frame update
    void Start()
    {
        TelemetryDistributor = GetComponent<TelemetryDistributor>();
        TelemetryRequester = new TelemetryRequester();
        TelemetryDistributor.Init(TelemetryRequester);
        TelemetryDistributor.SubscribeNewTelemetryDataListener(PrintTelemetryData);
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
