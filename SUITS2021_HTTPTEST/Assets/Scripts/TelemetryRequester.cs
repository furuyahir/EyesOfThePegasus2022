using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;
using UnityEngine;

public class TelemetryRequester
{
    private HttpClient client = new HttpClient();
    private Timer timer;
    private EventHandler<TelemetryData> telemetryDataReceivedEventHandler;

    public void SubscribeTelemetryReceivedListener(EventHandler<TelemetryData> listener)
    {
        telemetryDataReceivedEventHandler += listener;
    }

    public void RemoveTelemetryReceivedListener(EventHandler<TelemetryData> listener)
    {
        telemetryDataReceivedEventHandler -= listener;
    }
    
    private async void GetTelemetry()
    {
        string responseString = await client.GetStringAsync("http://localhost:3000/api/simulation/state");
        TelemetryData newData = TelemetryData.FromJson(responseString);
        telemetryDataReceivedEventHandler?.Invoke(this, newData);
    }
    
    public async void StartPolling()
    {
        await client.PostAsync("http://localhost:3000/api/simulation/start", new StringContent
        ("")).ConfigureAwait(true);
        timer = new Timer(1000) {AutoReset = true};
        timer.Elapsed += (sender, args) => GetTelemetry();
        timer.Start();
        Debug.Log("Polling Started");
    }
    
    public async void StopPolling()
    {
        timer?.Stop();
        await client.PostAsync("http://localhost:3000/api/simulation/stop", new StringContent("")).ConfigureAwait(true);
        Debug.Log("Polling Stopped");
    }
}
