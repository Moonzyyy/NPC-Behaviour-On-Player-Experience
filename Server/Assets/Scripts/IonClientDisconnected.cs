using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IonClientDisconnected
{
    void OnClientDisconnected(ulong clientID);
}
