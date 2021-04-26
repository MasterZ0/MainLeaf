using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour {
    [EventRef]
    public string[] audioPath;

    StudioEventEmitter sound;
    EventInstance music;

    private void Awake() {
        FMODUnity.StudioEventEmitter k;
        //music = RuntimeManager.CreateInstance(musicPath);
        music.setParameterByName("GameStarted", 1f);
    }
    public void Play(string path) {

        RuntimeManager.PlayOneShot(path, transform.position);
    }
}
