using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class DemoTriggerFollowMe : MonoBehaviour
{
    public VoiceInputManager VoiceInputManager;
    public FollowMeToggle FollowMeToggle;
    
    // Start is called before the first frame update
    void Start()
    {
        if (VoiceInputManager != null && FollowMeToggle != null)
        {
            VoiceInputManager.AddInputCommand(InputAction.Create("Follow", KeyCode.A,
                "Toggles Follow Me",
                () =>
                {
                    FollowMeToggle.ToggleFollowMeBehavior();
                }));
        }
        
        VoiceInputManager.EnableInput();
    }

}
