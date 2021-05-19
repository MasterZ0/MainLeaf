using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

[ExecuteAlways]
public class SceneLoader : MonoBehaviour {

    #region Variables and Properties

    [Header("GameManager")]
    [SerializeField] private float loadingLerpSpeed;
    [SerializeField] private Slider progressBar;
    [SerializeField] private bool building;

    [ListDrawerSettings(IsReadOnly = true)]
    public List<ScenePack> scenes = new List<ScenePack>(2) {
        new ScenePack(SceneIndexes.MainMenu),
        new ScenePack(SceneIndexes.Gameplay)
    };

    // Scene Manager
    private List<string> loadedStaticScenes = new List<string>();
    private List<AsyncOperation> loadingOperations = new List<AsyncOperation>();
    private ScenePack currentScene;

    public static Action<SceneIndexes> OnLoadEnd;

    #endregion

    #region Init

    private void Awake() {
#if !UNITY_EDITOR
        InitBuild();
#else
        if (Application.isPlaying) {
            InitEditor();        
        }
        else {
            OpenLinkedScenes();
        }
#endif
    }

    private void InitBuild() {
        ScenePack firstScene = scenes[0];
        LoadDynamicScenes(firstScene);
        LoadStaticScenes(firstScene);
        currentScene = firstScene;
        StartCoroutine(GetSceneLoadProgress());
    }

    private void InitEditor() { // Saves the scenes loaded in editor mode
        string sceneName = SceneManager.GetActiveScene().name;
        currentScene = scenes.First(s => s.name == sceneName);
        foreach (string staticScene in currentScene.staticScenes) {
            loadedStaticScenes.Add(staticScene);

        }
    }

#if UNITY_EDITOR
    private void OpenLinkedScenes() {
        if (building) {
            print("Building Option Enabled");
            return;
        }

        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene == SceneManager.GetSceneByBuildIndex(0))
            return;

        ScenePack current = scenes.First(scene => scene.name == activeScene.name);
        string[] linkedScenes = current.dynamicScenes.Union(current.staticScenes).ToArray();
        foreach (string s in linkedScenes) {
            EditorSceneManager.OpenScene(s, OpenSceneMode.Additive);
        }
    }
#endif

    #endregion

    public void LoadScene(SceneIndexes sceneIndex) {
        LoadScene(scenes[(int)sceneIndex]);
    }
    public void ReloadScene() {
        LoadScene(currentScene);
    }

    #region Scene Manager
    private void LoadScene(ScenePack newScene) {
        progressBar.value = 0;
        Time.timeScale = 0f;
        loadingOperations.Clear();

        UnloadDynamicScenes();
        LoadDynamicScenes(newScene);

        // Limpar a memoria
        ObjectPooler.Instance.Clear(currentScene == newScene);
        // UnloadSceneOptions unloadOption = sameScene ? UnloadSceneOptions.None : UnloadSceneOptions.UnloadAllEmbeddedSceneObjects;
        // Resources.UnloadUnusedAssets();   !sameScene
        GC.Collect();

        if (currentScene != newScene) {
            UnloadUnusedStaticScenes(newScene);
            LoadStaticScenes(newScene);
            currentScene = newScene;
        }

        StartCoroutine(GetSceneLoadProgress());
    }

    private void UnloadDynamicScenes() {    // Descarrega as atuais scenas dinamicas
        loadingOperations.Add(SceneManager.UnloadSceneAsync(currentScene.scene));
        foreach (string dynamicScene in currentScene.dynamicScenes) {
            loadingOperations.Add(SceneManager.UnloadSceneAsync(dynamicScene));
        }
    }

    private void LoadDynamicScenes(ScenePack newScene) { // Carregar a cena principal e suas cenas linkadas
        loadingOperations.Add(SceneManager.LoadSceneAsync(newScene.scene, LoadSceneMode.Additive));
        foreach (string dynamicScene in newScene.dynamicScenes) {
            loadingOperations.Add(SceneManager.LoadSceneAsync(dynamicScene, LoadSceneMode.Additive));
        }
    }
    private void UnloadUnusedStaticScenes(ScenePack newScene) {    // Verifica a lista das cenas estáticas carregas, e remove as que não serão usadas
        List<string> newSceneList = new List<string>(newScene.staticScenes);
        for (int i = 0; i < loadedStaticScenes.Count; i++) {
            if (!newSceneList.Contains(loadedStaticScenes[i])) {
                loadingOperations.Add(SceneManager.UnloadSceneAsync(loadedStaticScenes[i]));
                loadedStaticScenes.Remove(loadedStaticScenes[i]);
            }
        }
    }
    private void LoadStaticScenes(ScenePack newScene) {

        // Verifica a nova lista de cenas estáticas
        for (int i = 0; i < newScene.staticScenes.Length; i++) {

            if (!loadedStaticScenes.Contains(newScene.staticScenes[i])) {   // Caso ela já não estiver no jogo, carregue-a
                loadingOperations.Add(SceneManager.LoadSceneAsync(newScene.staticScenes[i], LoadSceneMode.Additive));
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
        SceneManager.SetActiveScene(SceneManager.GetSceneByPath(currentScene.scene));
        Time.timeScale = 1f;
        progressBar.value = 1;
        yield return new WaitForFixedUpdate();

        OnLoadEnd.Invoke(currentScene.SceneIndex);
    }
    #endregion
}
