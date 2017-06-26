using UnityEngine;

public interface Openable {
    
    bool IsLocked();

    void ToggleLock();
    void ToggleOpen();

    bool CanOpen();
    bool CanClose();

    void Open();
    void Close();
}
