/*
 
Copyright (c) Microsoft Corporation.

MIT License

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/

using System;
using System.Collections;
using System.Linq;
using UnityEngine.XR.WSA.Input;
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Maps inputs to actions.
/// </summary>
[Serializable]
public class InputAction
{
    #region Unity Inspector Fields
    [Tooltip("The phrase that can be used to trigger the action.")]
    [SerializeField]
    private string phrase;

    [Tooltip("The key that can be used to trigger the action.")]
    [SerializeField]
    private KeyCode key;

    [Tooltip("A description of the action to be displayed in help.")]
    [SerializeField]
    private string description;

    [Tooltip("The event that will be raised to handle the action.")]
    [SerializeField]
    private UnityEvent handler = new UnityEvent();
    #endregion // Unity Inspector Fields

    #region Public Methods
    /// <summary>
    /// Quick static helper to create input actions.
    /// </summary>
    /// <param name="phrase">
    /// The phrase that will trigger the action.
    /// </param>
    /// <param name="key">
    /// The key that will trigger the action.
    /// </param>
    /// <param name="description">
    /// A description of the action for help.
    /// </param>
    /// <param name="handler">
    /// A method that will handle the action.
    /// </param>
    /// <returns>
    /// The created input action.
    /// </returns>
    static public InputAction Create(string phrase, string description, UnityAction handler)
    {
        // Create th
        InputAction ia = new InputAction()
        {
            Phrase = phrase,
            Description = description,
        };
        ia.Handler.AddListener(handler);
        return ia;
    }
    #endregion // Public Methods

    #region Public Properties
    /// <summary>
    /// Gets or sets a description of the action.
    /// </summary>
    public string Description { get => description; set => description = value; }

    /// <summary>
    /// Gets or sets the key that can be used to trigger the action.
    /// </summary>
    public KeyCode Key { get => key; set => key = value; }

    /// <summary>
    /// Gets or sets the phrase that can be used to trigger the action.
    /// </summary>
    public string Phrase { get => phrase; set => phrase = value; }

    /// <summary>
    /// Gets the event that will be raised to handle the action.
    /// </summary>
    public UnityEvent Handler { get => handler; set => handler = value; }
    #endregion // Public Properties
}

public class VoiceInputManager : MonoBehaviour, IVoiceInputManager
{
    #region Member Variables

    public bool isEnabled;
    public AudioSource AudioSource;
    public AudioClip ConfirmationSoundClip;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<KeyCode, InputAction> keyMap = new Dictionary<KeyCode, InputAction>();
    private Dictionary<string, InputAction> speechMap = new Dictionary<string, InputAction>();
    private GameObject suMinimap = null;
    #endregion // Member Variables

    [Tooltip("Whether or not to include default commands.")]
    [SerializeField]
    private bool useDefaultCommands = true;

    [Space(10)]
    [Header("Events")]
    [Tooltip("Raised when input is enabled.")]
    [SerializeField]
    private UnityEvent inputEnabled = new UnityEvent();

    [Tooltip("Raised when input is disabled.")]
    [SerializeField]
    private UnityEvent inputDisabled = new UnityEvent();

    [Tooltip("Raised when command recognized.")]
    [SerializeField]
    private UnityEvent commandRecognized = new UnityEvent();

    [Space(10)]
    [Tooltip("The list of inputs and their respective actions.")]
    [SerializeField]
    private List<InputAction> inputActions = new List<InputAction>();

    #region Internal Methods
    /// <summary>
    /// Ads default command used in all scenes.
    /// </summary>
    // private void AddDefaultCommands()
    // {
    //     inputActions.Add(InputAction.Create("Toggle Scene Objects", KeyCode.Alpha1, 
    //         "Show / hide processed scene objects", () => { }));
    // }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        // Try to map from phrase to action
        InputAction action;
        if (speechMap.TryGetValue(args.text, out action))
        {
            // Mapped action found, notify
            Debug.Log("Phrase '" + args.text + "' recognized");
            action.Handler.Invoke();
            PlaySound(ConfirmationSoundClip);
            CommandRecognized?.Invoke();
        }
    }

    private void PlaySound(AudioClip sound)
    {
        AudioSource.PlayOneShot(sound);
    }

    #endregion // Internal Methods

    #region Public Methods
    public void AddVoiceCommand(InputAction inputAction)
    {
        inputActions.Add(inputAction);
    }

    /// <summary>
    /// Starts listening for input
    /// </summary>
    public void EnableInput()
    {
        // If already enabled, ignore
        if (isEnabled) { return; }

        // Enabled now
        isEnabled = true;

        // Map inputs to actions
        foreach (InputAction ia in inputActions)
        {
            // Map speech
            speechMap[ia.Phrase] = ia;

            // Map keys
            if (ia.Key != KeyCode.None) { keyMap[ia.Key] = ia; }
        }

        // Start listening for voice commands
        keywordRecognizer = new KeywordRecognizer(speechMap.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnPhraseRecognized;
        keywordRecognizer.Start();

        // Notify
        InputEnabled.Invoke();
    }

    /// <summary>
    /// Stops listening for input
    /// </summary>
    public void DisableInput()
    {
        // If not enabled, ignore
        if (!isEnabled) { return; }

        // No longer enabled
        isEnabled = false;

        // Stop listening for voice commands
        if (keywordRecognizer != null)
        {
            keywordRecognizer.Stop();
            keywordRecognizer.OnPhraseRecognized -= OnPhraseRecognized;
            keywordRecognizer = null;
        }

        // Clear input maps
        speechMap.Clear();
        keyMap.Clear();

        // Notify
        InputDisabled.Invoke();
    }
    #endregion // Public Methods

    #region Public Properties
    /// <summary>
    /// Gets or sets the list of inputs and their respective actions.
    /// </summary>
    public List<InputAction> InputActions { get => inputActions; set => inputActions = value; }

    /// <summary>
    /// Gets or sets a value that indicates whether or not to include default commands.
    /// </summary>
    public bool UseDefaultCommands { get => useDefaultCommands; set => useDefaultCommands = value; }

    #endregion // Public Properties

    #region Public Events
    /// <summary>
    /// Raised when input is enabled.
    /// </summary>
    public UnityEvent InputEnabled { get => inputEnabled; }

    /// <summary>
    /// Raised when input is disabled.
    /// </summary>
    public UnityEvent InputDisabled { get => inputDisabled; }

    public UnityEvent CommandRecognized
    {
        get => commandRecognized;
    }
    #endregion // Public Events
}

