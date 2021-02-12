using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class SafeTelemetryQueue : ITelemetryQueue
{
    private ConcurrentQueue<TelemetryData> telemetrySafeQueue;
    
    public SafeTelemetryQueue(ITelemetryRequester requester)
    {
        telemetrySafeQueue = new ConcurrentQueue<TelemetryData>();
        requester.SubscribeTelemetryReceivedListener(AddTelemetry);
    }

    public bool DequeueTelemetry(out TelemetryData data)
    {
        return telemetrySafeQueue.TryDequeue(out data);
    }

    private void AddTelemetry(object sender, TelemetryData data)
    {
        telemetrySafeQueue.Enqueue(data);
    }
}

public interface ITelemetryQueue
{
    bool DequeueTelemetry(out TelemetryData data);
}
