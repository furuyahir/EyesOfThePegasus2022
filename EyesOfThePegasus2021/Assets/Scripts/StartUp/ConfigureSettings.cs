using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Experimental.UI;

public class ConfigureSettings : MonoBehaviour
{
    MixedRealityKeyboard MixedRealityKeyboard;

    // Start is called before the first frame update
    void Start()
    {
        MixedRealityKeyboard = gameObject.AddComponent<MixedRealityKeyboard>();
        MixedRealityKeyboard.ShowKeyboard();
        MixedRealityKeyboard.OnCommitText.AddListener(OnKeyboardCommit);
    }

    private void OnKeyboardCommit()
    {
        PlayerPrefs.SetString(TelemetryRequester.URLPlayerPrefsKey, MixedRealityKeyboard.Text);
    }

}
