using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables and Properties
    public struct ScenePack {
        public SceneAsset scene;
        public SceneAsset[] linkedScenes;
    }


    [Header("GameManager")]
    //[SerializeField] private bool testMode;
    [SerializeField] private ScenePack linkedScenes;
    [SerializeField] private Animator transitionAnimator;

    private Action fadeInCallback;
    private Action fadeOutCallback;
    public static GameManager Instance { get; private set; }

    private int currentScenePack;
    private string currentScene;
    #endregion

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)] //(RuntimeInitializeLoadType.BeforeSceneLoad) ou Assemblies?
    private static void StartUp() {
        GameManager gameManager = Resources.Load<GameManager>(Constants.Path.GAME_MANEGER);
        Instance = Instantiate(gameManager);
        DontDestroyOnLoad(Instance.gameObject);
    }


    #region Init Scene
    public void SetTransitionCallback(Action openCallback) {
        fadeInCallback = openCallback;
    }
    #endregion

    #region Animation Events
    public void OnFadeInEnd() {
        fadeInCallback();
    }
    public void OnFadeOutEnd() {
        fadeOutCallback();
    }
    #endregion

    #region Scene Manager
    public void LoadNewScene(int packIndex) {
        currentScenePack = packIndex;
        transitionAnimator.Play(Constants.Anim.FADE_OUT);
        fadeOutCallback = () => StartCoroutine(LoadBasic());
    }
    public void ReloadScene() {
        transitionAnimator.Play(Constants.Anim.FADE_OUT);
        fadeOutCallback = () => StartCoroutine(LoadBasic());
    }

    IEnumerator LoadBasic() {

        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        while (!operation.isDone) {
            yield return null;
        }
        transitionAnimator.Play(Constants.Anim.FADE_IN);
    }

    IEnumerator LoadAsynchronously() {
        // currentScenePack[]
        bool sameScene = false;
        // Carregar nova cena
        AsyncOperation operation = SceneManager.UnloadSceneAsync(currentScene, sameScene ? UnloadSceneOptions.None : UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        while (!operation.isDone) {
            yield return null;
        }

        // Limpar a memoria
        ObjectPooler.Instance.Clear(sameScene);
        if (!sameScene)
            Resources.UnloadUnusedAssets();
        GC.Collect();

        // Carregar nova cena
        operation = SceneManager.LoadSceneAsync(000000, LoadSceneMode.Additive);
        operation.allowSceneActivation = false;

        while (!operation.isDone) {
            yield return null;

            if (operation.progress >= 0.9f) {
                operation.allowSceneActivation = true;
            }
        }

        // Iniciar cena
        transitionAnimator.Play(Constants.Anim.FADE_IN);
    }
    #endregion
}
