using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class InterfaceInitializer : MonoBehaviour
{
    #region Preset interface names

    private const string TelemetryNearDisplayName = "TelemetryNearDisplay";
    private const string TelemetrySonificationName = "TelemetrySonification";
    private const string WIMName = "WIM";

    #endregion

    #region Interface Prefabs

    public TelemetryNearDisplay TelemetryNearDisplayPrefab;
    public VoiceInputManager VoiceInputManagerPrefab;
    public WIM WIMPrefab;

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

    private void InitializeVoiceCommands(VoiceInputManager voiceInputManager, 
    List<IVoiceCommandable> voiceCommandables)
    {
        foreach (IVoiceCommandable voiceCommandable in voiceCommandables)
        {
            voiceCommandable.RegisterVoiceCommands(voiceInputManager);
        }

        voiceInputManager.EnableInput();
    }

    private void InstantiateUI(VoiceInputManager voiceInputManager,
        TelemetryDistributor telemetryDistributor, ITelemetryRequester telemetryRequester)
    {
        List<IVoiceCommandable> voiceCommandables = new List<IVoiceCommandable>();

        OpUICenter opUICenter = new OpUICenter();
        voiceCommandables.Add(opUICenter);
        
        TelemetryNearDisplay telemetryNearDisplay = Instantiate(TelemetryNearDisplayPrefab);
        telemetryNearDisplay.Init(telemetryDistributor);
        opUICenter.RegisterOpUI(telemetryNearDisplay, TelemetryNearDisplayName);
        voiceCommandables.Add(telemetryNearDisplay);

        AudioGenerator audioGenerator =
            new GameObject("Audio Generator").AddComponent<AudioGenerator>();
        audioGenerator.Init();
        TelemetrySonification telemetrySonification =
            new TelemetrySonification(audioGenerator, 400, 90, 250, 550, 75, 105);
        opUICenter.RegisterOpUI(telemetrySonification, TelemetrySonificationName);
        
        TelemetryTester telemetryTester = new GameObject("TelemetrySonificationTester")
            .AddComponent<TelemetryTester>();
        telemetryTester.Init(telemetryDistributor, telemetrySonification, telemetryRequester);

        WIM wim = Instantiate(WIMPrefab);
        voiceCommandables.Add(wim);
        opUICenter.RegisterOpUI(wim, WIMName);
        
        InitializeVoiceCommands(voiceInputManager, voiceCommandables);
    }
}
