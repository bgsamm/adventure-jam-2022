using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> activeLoops = new List<EventInstance>();

    /// <summary>
    /// Passing a null event will simply stop the current loop
    /// </summary>
    public void PlayLoop(EventReference eventRef, bool stopCurrent = true) {
        // stop current event instance
        if (stopCurrent) {
            foreach (var instance in activeLoops) {
                instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                instance.release();
            }
            activeLoops.Clear();
        }
        // start new event
        if (!eventRef.IsNull) {
            var instance = RuntimeManager.CreateInstance(eventRef);
            instance.start();
            activeLoops.Add(instance);
        }
    }

    public void PlayOneShot(EventReference eventRef) {
        if (!eventRef.IsNull)
            RuntimeManager.PlayOneShot(eventRef);
    }

    public void SetGlobalParameter(string name, int value) {
        RuntimeManager.StudioSystem.setParameterByName(name, value);
    }
}
