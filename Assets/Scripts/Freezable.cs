using UnityEngine;
using System.Collections;

public interface Freezable{
    void Freeze(float t);
    bool IsFrozen();
}
