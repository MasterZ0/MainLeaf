using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

[CustomPropertyDrawer(typeof(SceneRef))]
public class SceneRefDrawer : PropertyDrawer {

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        SceneAsset oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(property.stringValue);

        EditorGUI.BeginChangeCheck();
        SceneAsset newScene = EditorGUILayout.ObjectField(property.name, oldScene, typeof(SceneAsset), false) as SceneAsset;

        if (EditorGUI.EndChangeCheck()) {
            string newPath = AssetDatabase.GetAssetPath(newScene);
            property.stringValue = newPath;
        }
    }
}