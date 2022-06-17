using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private EventInstance currentLoop;

    public void PlayLoop(EventReference eventRef) {
        // stop current event instance
        currentLoop.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        currentLoop.release();
        // start new event
        currentLoop = RuntimeManager.CreateInstance(eventRef);
        currentLoop.start();
    }

    public void PlayOneShot(EventReference eventRef) {
        RuntimeManager.PlayOneShot(eventRef);
    }
}
