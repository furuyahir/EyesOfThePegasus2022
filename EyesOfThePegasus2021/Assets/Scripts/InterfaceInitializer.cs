using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class InterfaceInitializer : MonoBehaviour
{
    #region Preset interface names

    private const string TelemetryNearDisplayName = "TelemetryNearDisplay";
    private const string TelemetrySonificationName = "TelemetrySonification";

    #endregion

    #region Interface Prefabs

    public TelemetryNearDisplay TelemetryNearDisplayPrefab;
    public VoiceInputManager VoiceInputManagerPrefab;

    #endregion

    public GameObject TelemetryGameObject;
    private TelemetryDistributor telemetryDistributor;
    private ITelemetryRequester telemetryRequester;

    private string TelemetryURL;

    // Start is called before the first frame update
    void Start()
    {
        VoiceInputManager voiceInputManager = Instantiate(VoiceInputManagerPrefab);

        TelemetryGameObject = new GameObject("Telemetry GameObject");
        telemetryDistributor = TelemetryGameObject.AddComponent<TelemetryDistributor>();
        TelemetryURL = PlayerPrefs.GetString("TelemetryURL", null);
        telemetryRequester = new TelemetryRequester(TelemetryURL);
        
        telemetryDistributor.Init(telemetryRequester);

        InstantiateUI(voiceInputManager, telemetryDistributor, telemetryRequester);
    }

    [ContextMenu("Trigger Start")]
    private void StartProcesses()
    {
        telemetryRequester.StartPolling();
    }

    [ContextMenu("Stop Processes")]
    private void StopProcesses()
    {
        telemetryRequester.StopPolling();
    }

    private void InitializeVoiceCommands(VoiceInputManager voiceInputManager, OpUICenter opUICenter, TelemetryNearDisplay telemetryNearDisplay)
    {
        opUICenter.RegisterVoiceCommands(voiceInputManager);
        telemetryNearDisplay.RegisterVoiceCommands(voiceInputManager);

        voiceInputManager.EnableInput();
    }

    private void InstantiateUI(VoiceInputManager voiceInputManager,
        TelemetryDistributor telemetryDistributor, ITelemetryRequester telemetryRequester)
    {
        OpUICenter opUICenter = new OpUICenter();

        TelemetryNearDisplay telemetryNearDisplay = Instantiate(TelemetryNearDisplayPrefab);
        telemetryNearDisplay.Init();
        opUICenter.RegisterOpUI(telemetryNearDisplay, TelemetryNearDisplayName);

        AudioGenerator audioGenerator =
            new GameObject("Audio Generator").AddComponent<AudioGenerator>();
        audioGenerator.Init();
        TelemetrySonification telemetrySonification =
            new TelemetrySonification(audioGenerator, 400, 90, 250, 550, 75, 105);
        opUICenter.RegisterOpUI(telemetrySonification, TelemetrySonificationName);
        
        TelemetryTester telemetryTester = new GameObject("TelemetrySonificationTester")
            .AddComponent<TelemetryTester>();
        telemetryTester.Init(telemetryDistributor, telemetrySonification, telemetryRequester);
        
        InitializeVoiceCommands(voiceInputManager, opUICenter, telemetryNearDisplay);
    }
}
