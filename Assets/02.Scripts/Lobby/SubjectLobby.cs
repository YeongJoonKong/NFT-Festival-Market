using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SubjectLobby
{
    void AddObserver(ObserverLobby observer);
    void RemoveObserver(ObserverLobby observer);
    void NotifyObserver();
}
