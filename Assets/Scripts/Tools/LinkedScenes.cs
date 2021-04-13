using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;


[ExecuteAlways]
public class LinkedScenes : MonoBehaviour {
    [SerializeField]
    SceneAsset[] linkedScenes;

#if UNITY_EDITOR
    void Start() {
        if (Application.isPlaying)
            Destroy(gameObject);

        Load();
    }

    private void OnValidate() {
        Load();
    }
    private void Load() {
        print("Auto load");
        if (linkedScenes == null)
            return;
        
        string[] sceneNames = linkedScenes.Select(l => l.name).ToArray();

        if (sceneNames != null && sceneNames.Length > 0) {
            int countLoaded = SceneManager.sceneCount;
            Scene[] loadedScenes = new Scene[countLoaded];
            for (int i = 0; i < countLoaded; i++)
                loadedScenes[i] = SceneManager.GetSceneAt(i);
            for (int i = 0; i < sceneNames.Length; ++i) {
                // discard scene if it's already loaded
                if (loadedScenes.Any(s => s.name == sceneNames[i]))
                    continue;

                print(AssetDatabase.GetAssetPath(linkedScenes[i]));
                EditorSceneManager.OpenScene(Constants.Path.PERSISTENT_SCENE, OpenSceneMode.Additive);
            }

            EditorSceneManager.SetActiveScene(EditorSceneManager.GetSceneAt(0));
        }
    }

#endif
}

