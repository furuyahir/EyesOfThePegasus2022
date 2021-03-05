using System;
using UnityEngine;

public class TelemetryDistributor : MonoBehaviour, ITelemetryDistributor
{
    private ITelemetryQueue telemetryQueue;
    private bool initialized;
    private event EventHandler<TelemetryData> NewTelemetryDataReceived;
    private double lastTimestamp;
    
    public void Init(ITelemetryRequester telemetryRequester)
    {
        telemetryQueue = new SafeTelemetryQueue(telemetryRequester);
        lastTimestamp = Double.NegativeInfinity;
        initialized = true;
    }

    public void SubscribeNewTelemetryDataListener(EventHandler<TelemetryData> listener)
    {
        NewTelemetryDataReceived += listener;
    }

    public void RemoveNewTelemetryDataListener(EventHandler<TelemetryData> listener)
    {
        NewTelemetryDataReceived -= listener;
    }

    private bool TelemetryDataIsNew(TelemetryData data)
    {
        return data.time > lastTimestamp;
    }

    private void InvokeNewTelemetryDataReceived(TelemetryData data)
    {
        NewTelemetryDataReceived?.Invoke(this, data);
    }

    void Update()
    {
        if (!initialized)
        {
            Debug.Log("not initialized");
            return;
        }

        TelemetryData newData;
        if (telemetryQueue.DequeueTelemetry(out newData))
        {
            if (TelemetryDataIsNew(newData))
            {
                InvokeNewTelemetryDataReceived(newData);
            }
        }
    }
}

public interface ITelemetryDistributor
{
    void Init(ITelemetryRequester telemetryRequester);
    void SubscribeNewTelemetryDataListener(EventHandler<TelemetryData> listener);
    void RemoveNewTelemetryDataListener(EventHandler<TelemetryData> listener);
}
