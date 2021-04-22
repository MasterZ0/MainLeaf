using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {
    [Serializable]
    private struct Sound {
        [EventRef]
        public string musicPath;
        public Music music;
    }

    [SerializeField] private Sound[] musics;

    private Music currentMusic = Music.None;
    private EventInstance musicEvent;

    public void ChangeMusic(Music music) {
        if (currentMusic == music)
            return;

        musicEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        currentMusic = music;

        if (currentMusic == Music.None)
            return;        

        string path = Array.Find(musics, m => m.music == music).musicPath;
        musicEvent = RuntimeManager.CreateInstance(path);
        musicEvent.start();
        musicEvent.release();
    }


    //[EventRef]
    //public string musicPath;
    //EventInstance music;

    //public void Test() {
        //music = RuntimeManager.CreateInstance(musicPath);
    //    music.start();

    //    // after a while
    //    music.setParameterByName("GameStarted", 1f);
    //}

    //StudioEventEmitter sound;
    //public void Test2() {

    //}

}
