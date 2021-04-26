using System;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIAudio : MonoBehaviour {

    [Serializable]
    private struct Sound {
        [EventRef]
        public string sfxPath;
        public EventTriggerType eventType;
    }

    [SerializeField] private Sound[] uiSfx;

    private static EventInstance audioEvent;
    private static bool firstSelect;

    public void Play(EventTriggerType eventType) {
        string path = Array.Find(uiSfx, s => s.eventType == eventType).sfxPath;
        audioEvent = RuntimeManager.CreateInstance(path);
        audioEvent.start();
    }

    public void OnSubmit(Selectable button) {
        OnSubmit();
        button?.Select();
    }
    public void OnSubmit() {
        firstSelect = true;
        Play(EventTriggerType.Submit);
    }
    public void OnSelect() {
        if (firstSelect) {
            firstSelect = false;
            return;
        }
        Play(EventTriggerType.Select);
    }
    public void OnCancel(Selectable button) {
        OnCancel();
        button?.Select();
    }
    public void OnCancel() {
        firstSelect = true;
        Play(EventTriggerType.Cancel);
    }
}
