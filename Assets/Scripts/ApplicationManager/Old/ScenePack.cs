using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScenePack {

    public ScenePack(/*SceneIndexes*/ int scene) {
        SceneIndex = scene;
    }

    public int SceneIndex { get; }
    public string name { get => SceneIndex.ToString(); }

    [Title("$name")]
    //[SceneRef]
    public string scene;
    //[SceneRef]
    public string[] dynamicScenes;  // Cenas que precisam ser recarregadas
    //[SceneRef]
    public string[] staticScenes;   // UI ou que não precisam ser recarregadas
}
