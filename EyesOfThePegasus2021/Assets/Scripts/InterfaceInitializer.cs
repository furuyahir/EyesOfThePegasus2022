using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class InterfaceInitializer : MonoBehaviour
{
    #region Preset interface names

    private const string TelemetryNearDisplayName = "TelemetryNearDisplay";

    #endregion

    #region Interface Prefabs

    public TelemetryNearDisplay TelemetryNearDisplayPrefab;
    public VoiceInputManager VoiceInputManagerPrefab;

    #endregion

    public GameObject TelemetryGameObject;
    private TelemetryDistributor telemetryDistributor;
    private TelemetryRequester telemetryRequester;

    private string TelemetryURL;

    void Awake()
    {
        TelemetryURL = PlayerPrefs.GetString(TelemetryRequester.URLPlayerPrefsKey, null);
    }

    // Start is called before the first frame update
    void Start()
    {
        VoiceInputManager voiceInputManager = Instantiate(VoiceInputManagerPrefab);


        TelemetryGameObject = new GameObject();
        telemetryDistributor = TelemetryGameObject.AddComponent<TelemetryDistributor>();
        telemetryRequester = new TelemetryRequester(TelemetryURL);
        telemetryDistributor.Init(telemetryRequester);

        InstantiateUI(voiceInputManager, telemetryRequester);
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
        TelemetryRequester telemetryRequester)
    {
        OpUICenter opUICenter = new OpUICenter();
        TelemetryNearDisplay telemetryNearDisplay = Instantiate(TelemetryNearDisplayPrefab);
        telemetryNearDisplay.Init();
        opUICenter.RegisterOpUI(telemetryNearDisplay, TelemetryNearDisplayName);

        InitializeVoiceCommands(voiceInputManager, opUICenter, telemetryNearDisplay);
    }
}
