using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigureSettins : MonoBehaviour
{
    UnityEngine.TouchScreenKeyboard keyboard;
    public static string keyboardText = "";

    // Start is called before the first frame update
    void Start()
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false);
    }

    

    // Update is called once per frame
    void Update()
    {
        if (TouchScreenKeyboard.visible == false && keyboard != null)
        {
            if (keyboard.status == TouchScreenKeyboard.Status.Done)
            {
                keyboardText = keyboard.text;
                keyboard = null;
            }
            if(!PlayerPrefs.HasKey("TelemetryURL"))
            {
                PlayerPrefs.SetString("TelemtryURL", keyboardText);
            }
            
        }
    }
}
