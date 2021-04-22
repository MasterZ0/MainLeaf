using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[ExecuteAlways]
public class GameManager : MonoBehaviour // Atualizar map != navmash
{
    #region Variables and Properties
    [Serializable]
    public class ScenePack {
        [HideInInspector]
        public string name; // Apenas pra organizar no inspector
        public SceneAsset scene;
        public SceneAsset[] dynamicScenes;  // Cenas que precisam ser recarregadas
        public SceneAsset[] staticScenes;   // UI ou que não precisam ser recarregadas
    }

    [Header("GameManager")]
    [SerializeField] private float loadingLerpSpeed;

    [Space]
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private Slider progressBar;
    [SerializeField] private ScenePack[] scenes;

    private Action fadeInCallback;
    private Action fadeOutCallback;

    // Scene Manager
    private List<SceneAsset> loadedStaticScenes = new List<SceneAsset>();
    private List<AsyncOperation> loadingOperations = new List<AsyncOperation>();
    private ScenePack currentScene;
    public static GameManager Instance { get; private set; }
    public static MusicManager MusicManager { get; private set; }

    #endregion

    private void Awake() {
#if !UNITY_EDITOR
        LoadScene(scenes[(int)SceneIndexes.MainMenu]);
#else
        if (!Application.isPlaying) {
            OpenFromEditor();
            return;
        }
        else {  // Saves the scenes loaded in the editor
            string sceneName = SceneManager.GetActiveScene().name;
            currentScene = scenes.First(s => s.name == sceneName);
            foreach (SceneAsset staticScene in currentScene.staticScenes) {
                loadedStaticScenes.Add(staticScene);

            }
        }
#endif
        Init();
    }

    private void Init() {
        Instance = this;
        MusicManager = GetComponent<MusicManager>();
        AudioSettings.Setup();
    }

    public void OpenFromEditor() {
        Scene scene = SceneManager.GetActiveScene();

        if (scene == SceneManager.GetSceneByBuildIndex(0))
            return;

        ScenePack current = scenes.First(s => s.name == scene.name);

        foreach (SceneAsset s in current.dynamicScenes) {
            EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(s), OpenSceneMode.Additive);
        }

        foreach (SceneAsset s in current.staticScenes) {
            EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(s), OpenSceneMode.Additive);
        }
    }

    private void OnValidate() {
        for (int i = 0; i < scenes.Length; i++) {
            scenes[i].name = ((SceneIndexes)i).ToString();
        }
    }

    #region Public Action/Request
    public void SetTransitionCallback(Action openCallback) {
        fadeInCallback = openCallback;
    }
    public void LoadNewScene(SceneIndexes sceneIndex) {
        transitionAnimator.Play(Constants.Anim.FADE_OUT);
        fadeOutCallback = () => LoadScene(scenes[(int)sceneIndex]);
    }
    public void ReloadScene() {
        transitionAnimator.Play(Constants.Anim.FADE_OUT);
        fadeOutCallback = () => LoadScene(currentScene);
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
    private void LoadScene(ScenePack newScene) {
        progressBar.value = 0;
        Time.timeScale = 0f;
        loadingOperations.Clear();

        LoadDynamicScenes(newScene);

        // Limpar a memoria
        ObjectPooler.Instance.Clear(currentScene == newScene);
        // UnloadSceneOptions unloadOption = sameScene ? UnloadSceneOptions.None : UnloadSceneOptions.UnloadAllEmbeddedSceneObjects;
        // Resources.UnloadUnusedAssets();   !sameScene
        GC.Collect();

        if (currentScene != newScene) {
            LoadStaticScenes(newScene);
            currentScene = newScene;
        }

        StartCoroutine(GetSceneLoadProgress());
    }

    private void LoadDynamicScenes(ScenePack newScene) {

        // Descarrega as atuais scenas dinamicas
        loadingOperations.Add(SceneManager.UnloadSceneAsync(currentScene.scene.name));
        foreach (SceneAsset dynamicScene in currentScene.dynamicScenes) {
            loadingOperations.Add(SceneManager.UnloadSceneAsync(dynamicScene.name));
        }

        // Carregar a cena principal e suas cenas linkadas
        loadingOperations.Add(SceneManager.LoadSceneAsync(newScene.scene.name, LoadSceneMode.Additive));
        foreach (SceneAsset dynamicScene in newScene.dynamicScenes) {
            loadingOperations.Add(SceneManager.LoadSceneAsync(dynamicScene.name, LoadSceneMode.Additive));
        }
    }
    private void LoadStaticScenes(ScenePack newScene) {
        // Verifica a lista das cenas estáticas carregas, e remove as que não serão usadas
        List<SceneAsset> newSceneList = new List<SceneAsset>(newScene.staticScenes);
        for (int i = 0; i < loadedStaticScenes.Count; i++) {
            if (!newSceneList.Contains(loadedStaticScenes[i])) {
                loadingOperations.Add(SceneManager.UnloadSceneAsync(loadedStaticScenes[i].name));
                loadedStaticScenes.Remove(loadedStaticScenes[i]);
            }
        }
        // Verifica a nova lista de cenas estáticas
        for (int i = 0; i < newScene.staticScenes.Length; i++) {

            if (!loadedStaticScenes.Contains(newScene.staticScenes[i])) {   // Caso ela já não estiver no jogo, carregue-a
                loadingOperations.Add(SceneManager.LoadSceneAsync(newScene.staticScenes[i].name, LoadSceneMode.Additive));
                loadedStaticScenes.Add(newScene.staticScenes[i]);
            }
        }
    }

    private IEnumerator GetSceneLoadProgress() {
        for (int i = 0; i < loadingOperations.Count; i++) {
            while (!loadingOperations[i].isDone) {
                float totalSceneProgress = 0;

                foreach (AsyncOperation operation in loadingOperations) {
                    totalSceneProgress += operation.progress;
                }
                progressBar.value = Mathf.Lerp(progressBar.value, totalSceneProgress / loadingOperations.Count, loadingLerpSpeed);
                //progressBar.value = totalSceneProgress / loadingOperations.Count; // round to int?
                yield return null;
            }
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentScene.scene.name));
        Time.timeScale = 1f;
        progressBar.value = 1;
        yield return new WaitForFixedUpdate();

        transitionAnimator.Play(Constants.Anim.FADE_IN);    // load fase
    }

    //private string[] tips;
    //private bool isLoading;
    //public int tipCount;
    //public TextMeshProUGUI tipsText;
    //private IEnumerator GenerateTips() {
    //    tipCount = tipCount.NewRandomIndex(tipCount);
    //    tipsText.text = tips[tipCount];
    //    while (isLoading) {
    //        yield return new WaitForSeconds(3f);

    //        LeanTween,alphaCanvas(alphaCanvas, 0, .5f);
    //        yield return new WaitForSeconds(.5f);

    //        tipCount = tipCount.NewRandomIndex(tipCount);
    //        LeanTween,alphaCanvas(alphaCanvas, 1, .5f);
    //    }
    //}

    #endregion

}

