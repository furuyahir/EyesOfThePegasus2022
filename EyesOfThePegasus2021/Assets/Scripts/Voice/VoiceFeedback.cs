using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceFeedback : MonoBehaviour
{
    public VoiceInputManager VoiceInputManager;

    public AudioSource AudioSource;

    public AudioClip ConfirmationSound;
    // Start is called before the first frame update
    void Start()
    {
        VoiceInputManager.CommandRecognized.AddListener(OnCommandRecognized);
    }

    void OnCommandRecognized()
    {
        AudioSource.PlayOneShot(ConfirmationSound);
    }
}
