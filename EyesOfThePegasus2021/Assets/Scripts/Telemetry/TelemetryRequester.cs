using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;
using UnityEngine;

public class TelemetryRequester : ITelemetryRequester
{
    private HttpClient client = new HttpClient();
    private Timer timer;
    private event EventHandler<TelemetryData> telemetryDataReceivedEventHandler;
    private const string DEFAULT_URL = "http://localhost:3000";
    public static readonly string URLPlayerPrefsKey = "TELEMETRY_URL";
    public string TelemetryURL { get; private set; }

    public TelemetryRequester(string url = null)
    {
        TelemetryURL = string.IsNullOrEmpty(url) ? DEFAULT_URL : url;
    }

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
        string responseString = await client.GetStringAsync($"{TelemetryURL}/api/simulation/state");
        TelemetryData newData = TelemetryData.FromJson(responseString);
        telemetryDataReceivedEventHandler?.Invoke(this, newData);
    }

    public async void StartPolling()
    {
        await client.PostAsync($"{TelemetryURL}/api/simulation/start", new StringContent
            ("")).ConfigureAwait(true);
        timer = new Timer(1000) { AutoReset = true };
        timer.Elapsed += (sender, args) => GetTelemetry();
        timer.Start();
        Debug.Log("Polling Started");
    }

    public async void StopPolling()
    {
        timer?.Stop();
        await client.PostAsync($"{TelemetryURL}/api/simulation/stop",
            new StringContent("")).ConfigureAwait(true);
        Debug.Log("Polling Stopped");
    }
}

public interface ITelemetryRequester
{
    void SubscribeTelemetryReceivedListener(EventHandler<TelemetryData> listener);
    void RemoveTelemetryReceivedListener(EventHandler<TelemetryData> listener);
    void StartPolling();
    void StopPolling();
}