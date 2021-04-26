using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Options : MonoBehaviour {

    [Header("Options")]
    [SerializeField] private GameObject externalPanel;
    [SerializeField] private GameObject firstPanel;
    [SerializeField] private Button firstTab;
    [SerializeField] private UIAudio uiAudio;

    private GameObject currentPanel;
    private Action onCloseCallback;
    private static Options Instance { get; set; }
    private void Awake() {
        Instance = this;
        currentPanel = firstPanel;
    }
    public static void OpenOption(Action closeCallback) => Instance.Open(closeCallback);    

    private void Open(Action closeCallback) {
        onCloseCallback = closeCallback;
        externalPanel.SetActive(true);
        firstTab.Select();
    }

    #region Buttons Event 
    public void OnCloseOptions() {
        currentPanel.SetActive(false);
        externalPanel.SetActive(false);
        onCloseCallback();
        uiAudio.OnCancel();
    }
    public void OnSelectTab(GameObject panel) {
        currentPanel.SetActive(false);
        panel.SetActive(true);
        currentPanel = panel;
        uiAudio.OnSelect();
    }

    #endregion
}