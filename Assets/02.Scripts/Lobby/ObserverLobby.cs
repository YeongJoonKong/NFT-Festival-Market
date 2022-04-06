using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ObserverLobby
{
    void DetectEvent(Object obj);
    void DetectEvent(string _event);
}
