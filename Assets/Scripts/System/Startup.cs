using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class Startup {
    static Startup() {
        EditorSceneManager.sceneOpened += LoadPersistentScene;
        EditorSceneManager.sceneSaving += Saving;
    }

    static void LoadPersistentScene(Scene scene, OpenSceneMode mode) {
        if (mode == OpenSceneMode.Single && scene.buildIndex > 0) {
            //string path = EditorSceneManager.GetSceneByBuildIndex(0).path;
            EditorSceneManager.OpenScene(Constants.Path.PERSISTENT_SCENE, OpenSceneMode.Additive);
        }
    }

    static void Saving(Scene scene, string path) {
        if(scene.name != SceneManager.GetActiveScene().name)
            Debug.LogError($"ALERTA! VOCÊ ACABOU DE SALVAR ALTERAÇÕES NA CENA '{scene.name}'");
    }
}