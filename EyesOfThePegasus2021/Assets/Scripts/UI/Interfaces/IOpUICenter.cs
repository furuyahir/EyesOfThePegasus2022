using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOpUICenter
{
    bool RegisterOpUI(IOpUI newOpUI, string name);
    bool RemoveOpUI(string name);
    bool TurnOnOpUI(string name);
    bool TurnOffOpUI(string name);
    void TurnOffAll();
    void Restore();
    void SubscribeOpUIAddedListener(EventHandler<OpUIEventArgs> listener);
    void RemoveOpUIAddedListener(EventHandler<OpUIEventArgs> listener);
    void SubscribeOpUIRemovedListener(EventHandler<OpUIEventArgs> listener);
    void RemoveOpUIRemovedListener(EventHandler<OpUIEventArgs> listener);
    Dictionary<string, IOpUI> GetRegisteredOpUIs();
}

public class OpUIEventArgs : EventArgs
{
    public IOpUI OpUI;
    public string Name;

    public OpUIEventArgs(IOpUI opUI, string name)
    {
        OpUI = opUI;
        Name = name;
    }
}