using UnityEngine;

public interface Openable {
    
    bool IsLocked();

    void ToggleLock();

    bool CanOpen();
    bool CanClose();

    void Open();
    void Close();
}
