using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour // Atualizar map != navmash
{
    #region Variables and Properties

    [Header("GameManager")]
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private SceneLoader sceneLoader;

    private Action fadeInCallback;
    private Action fadeOutCallback;
    public static GameManager Instance { get; private set; }
    public static MusicManager MusicManager { get; private set; }

    #endregion

    private void Awake() {
        Init();
    }

    private void Init() {
        Instance = this;
        SceneLoader.OnLoadEnd += OnLoadEnd;
        MusicManager = GetComponent<MusicManager>();
        AudioSettings.Setup();
    }

#region Public Action/Request
    public void SetTransitionCallback(Action openCallback) {
        fadeInCallback = openCallback;
    }
    public void LoadNewScene(SceneIndexes sceneIndex) {
        transitionAnimator.Play(Constants.Anim.FADE_OUT);
        fadeOutCallback = () => sceneLoader.LoadScene(sceneIndex);
    }
    public void ReloadScene() {
        transitionAnimator.Play(Constants.Anim.FADE_OUT);
        fadeOutCallback = () => sceneLoader.ReloadScene();
    }
#endregion

#region Animation Events
    public void OnLoadEnd(SceneIndexes sceneIndexes) {
        transitionAnimator.Play(Constants.Anim.FADE_IN);
    }
    public void OnFadeInEnd() {     // Init Scene
        fadeInCallback?.Invoke();
    }
    public void OnFadeOutEnd() {    // LoadScene
        fadeOutCallback();
    }
#endregion

}

