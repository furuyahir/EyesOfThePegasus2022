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

    private OpUICenter OpUICenter;

    private string TelemetryURL;

    void Awake()
    {
        TelemetryURL = PlayerPrefs.GetString("TelemetryURL", null);
    }

    // Start is called before the first frame update
    void Start()
    {
        OpUICenter = new OpUICenter();

        VoiceInputManager voiceInputManager = Instantiate(VoiceInputManagerPrefab);

        TelemetryGameObject = new GameObject();
        telemetryDistributor = TelemetryGameObject.AddComponent<TelemetryDistributor>();
        telemetryRequester = new TelemetryRequester(TelemetryURL);
        telemetryDistributor.Init(telemetryRequester);

        InstantiateUI(OpUICenter, voiceInputManager, telemetryRequester);
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

    private void InstantiateUI(OpUICenter opUICenter, VoiceInputManager voiceInputManager,
        TelemetryRequester telemetryRequester)
    {
        TelemetryNearDisplay telemetryNearDisplay = Instantiate(TelemetryNearDisplayPrefab);
        opUICenter.RegisterOpUI(telemetryNearDisplay, TelemetryNearDisplayName);
        telemetryNearDisplay.InitVoiceCommands(voiceInputManager);
    }
}
