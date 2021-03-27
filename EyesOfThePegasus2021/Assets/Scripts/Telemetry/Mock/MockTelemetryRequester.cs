using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockTelemetryRequester : MonoBehaviour, ITelemetryRequester
{
    private int[] heartRates1 = {90, 90, 90, 91, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100};
    private int[] heartRates2 = {85, 85, 87, 91, 92, 92, 92, 92, 92, 89, 88, 87, 85, 83};

    public bool UseList1 = true;
    
    private event EventHandler<TelemetryData> telemetryDataReceivedEventHandler;
    private Coroutine telemetryGeneratorCoroutine;

    private IEnumerator GenerateTelemetryData()
    {
        int idx = 0;
        int[] telemetryArray = UseList1 ? heartRates1 : heartRates2;
        while (idx < telemetryArray.Length)
        {
            TelemetryData newData = new TelemetryData()
            {
                time = idx,
                heart_bpm = telemetryArray[idx]
            };
            telemetryDataReceivedEventHandler?.Invoke(this, newData);
            idx++;
            yield return new WaitForSeconds(1);
        }
    }

    public void SubscribeTelemetryReceivedListener(EventHandler<TelemetryData> listener)
    {
        telemetryDataReceivedEventHandler += listener;
    }

    public void RemoveTelemetryReceivedListener(EventHandler<TelemetryData> listener)
    {
        telemetryDataReceivedEventHandler -= listener;
    }

    public void StartPolling()
    {
        if (telemetryGeneratorCoroutine != null)
        {
            StopCoroutine(telemetryGeneratorCoroutine);
        }

        telemetryGeneratorCoroutine = StartCoroutine(GenerateTelemetryData());
    }

    public void StopPolling()
    {
        if (telemetryGeneratorCoroutine != null)
        {
            StopCoroutine(telemetryGeneratorCoroutine);
        }

        telemetryGeneratorCoroutine = null;
    }
}
