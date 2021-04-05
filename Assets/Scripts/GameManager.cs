using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables and Properties
    [SerializeField] private Animator transitionAnimator;

    private Action transitionOpenCallback;
    private Action transitionTriggerCallback;
    public static GameManager Instance { get; private set; }

    private bool dontLoadScene;
    private string nextScene;
    private string unloadScene;
    #endregion

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)] //(RuntimeInitializeLoadType.BeforeSceneLoad) ou Assemblies?
    private static void StartUp() {
        GameManager gameManager = Resources.Load<GameManager>(Constants.Path.GAME_MANEGER);
        Instance = Instantiate(gameManager);
        DontDestroyOnLoad(Instance.gameObject);
    }
    private void Awake() {

    }

    public void SetTransitionCallback(Action openCallback) {
        transitionOpenCallback = openCallback;
    }
    public void SetTransitionCallback(Action openCallback, Action closeCallback) {
        transitionOpenCallback = openCallback;
        transitionTriggerCallback = closeCallback;
    }
    public void LoadScene(string currentScene, string newScene) {
        unloadScene = currentScene;
        nextScene = newScene;
        dontLoadScene = false;

        transitionAnimator.Play(Constants.Anim.FADE_OUT);
    }
    public void CloseTransition() {
        dontLoadScene = true;
        transitionAnimator.Play(Constants.Anim.FADE_OUT);
    }
    public void OpenTransition() {
        transitionAnimator.Play(Constants.Anim.FADE_IN);
    }

    public void OnFadeInEnd() {
        transitionOpenCallback();
    }
    public void OnFadeOutEnd() {
        if (dontLoadScene) {
            transitionTriggerCallback();   // MainMenu
            return;
        }

        StartCoroutine(LoadBasic());
    }
    IEnumerator LoadBasic() {

        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);
        while (!operation.isDone) {
            yield return null;
        }
        transitionAnimator.Play(Constants.Anim.FADE_IN);
    }

    IEnumerator LoadAsynchronously() {
        bool sameScene = nextScene == unloadScene;

        // Carregar nova cena
        AsyncOperation operation = SceneManager.UnloadSceneAsync(unloadScene, sameScene ? UnloadSceneOptions.None : UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        while (!operation.isDone) {
            yield return null;
        }

        // Limpar a memoria
        ObjectPooler.instance.Clear(sameScene);
        if (!sameScene)
            Resources.UnloadUnusedAssets();
        GC.Collect();

        // Carregar nova cena
        operation = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
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
}
