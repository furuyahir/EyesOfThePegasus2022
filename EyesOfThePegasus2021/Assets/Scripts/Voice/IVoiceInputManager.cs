using UnityEngine.Events;

public interface IVoiceInputManager
{
    void AddVoiceCommand(InputAction inputAction);
    void EnableInput();
    void DisableInput();
    UnityEvent CommandRecognized { get; }
    UnityEvent InputEnabled { get; }
    UnityEvent InputDisabled { get; }
}
