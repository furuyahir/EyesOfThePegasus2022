using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpUICenter : IOpUICenter
{
    private event EventHandler<OpUIEventArgs> OpUIAdded;
    private event EventHandler<OpUIEventArgs> OpUIRemoved;
    private Dictionary<string, IOpUI> OpUIs;
    private Dictionary<IOpUI, bool> OnTracker;

    public OpUICenter()
    {
        OpUIs = new Dictionary<string, IOpUI>();
        OnTracker = new Dictionary<IOpUI, bool>();
    }

    public bool RegisterOpUI(IOpUI newOpUI, string name)
    {
        bool returnValue = OpUIs.ContainsKey(name);
        if (!returnValue)
        {
            OpUIs.Add(name, newOpUI);
            InvokeOpUIAddedEvent(name, newOpUI);
        }
        else
        {
            Debug.LogWarning($"Attempted to add OpUI Name: {name} that already exists.");
        }

        return returnValue;
    }

    public void RegisterVoiceCommands(VoiceInputManager voiceInputManager)
    {
        voiceInputManager.AddVoiceCommand(InputAction.Create("Go Dark",
        "Turns off all UI elements", () =>
        {
            TurnOffAll();
        }));

        voiceInputManager.AddVoiceCommand(InputAction.Create("Restore",
        "Restores UI element status", () =>
        {
            Restore();
        }));
    }

    private void InvokeOpUIAddedEvent(string nameOfAddedOpUI, IOpUI addedOpUI)
    {
        OpUIAdded?.Invoke(this, new OpUIEventArgs(addedOpUI, nameOfAddedOpUI));
    }

    public bool RemoveOpUI(string name)
    {
        bool returnValue = OpUIs.ContainsKey(name);
        if (returnValue)
        {
            OpUIs[name].Remove();
            OpUIs.Remove(name);
            InvokeOpUIRemovedEvent(name);
        }

        return returnValue;
    }

    private void InvokeOpUIRemovedEvent(string nameOfRemovedOpUI)
    {
        OpUIAdded?.Invoke(this, new OpUIEventArgs(null, nameOfRemovedOpUI));
    }

    public bool TurnOnOpUI(string name)
    {
        IOpUI opUIToTurnOn;
        bool returnValue = OpUIs.TryGetValue(name, out opUIToTurnOn);
        if (returnValue)
        {
            opUIToTurnOn.TurnOn();
        }

        return returnValue;
    }

    public bool TurnOffOpUI(string name)
    {
        IOpUI opUIToTurnOn;
        bool returnValue = OpUIs.TryGetValue(name, out opUIToTurnOn);
        if (returnValue)
        {
            opUIToTurnOn.TurnOff();
        }

        return returnValue;
    }

    public void TurnOffAll()
    {
        foreach (IOpUI opUI in OpUIs.Values)
        {
            OnTracker[opUI] = opUI.IsOn();
            opUI.TurnOff();
        }
    }

    public void Restore()
    {
        foreach (IOpUI opUI in OnTracker.Keys)
        {
            if (OnTracker[opUI])
            {
                opUI.TurnOn();
            }
        }
    }

    public void SubscribeOpUIAddedListener(EventHandler<OpUIEventArgs> listener)
    {
        OpUIAdded += listener;
    }

    public void RemoveOpUIAddedListener(EventHandler<OpUIEventArgs> listener)
    {
        OpUIAdded -= listener;
    }

    public void SubscribeOpUIRemovedListener(EventHandler<OpUIEventArgs> listener)
    {
        OpUIRemoved += listener;
    }

    public void RemoveOpUIRemovedListener(EventHandler<OpUIEventArgs> listener)
    {
        OpUIRemoved -= listener;
    }

    public Dictionary<string, IOpUI> GetRegisteredOpUIs()
    {
        return new Dictionary<string, IOpUI>(OpUIs);
    }
}
