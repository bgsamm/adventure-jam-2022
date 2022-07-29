using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeSound : MonoBehaviour
{
    UnityAudioManager sfxManager => ResourceLocator.instance.SFXManager;

    void OnTriggerEnter2D(Collider2D col)
    {
        sfxManager.onBridge = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        sfxManager.onBridge = false;
    }
}
