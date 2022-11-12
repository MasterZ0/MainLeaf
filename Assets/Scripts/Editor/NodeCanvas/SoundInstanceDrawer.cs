using AdventureGame.Audio;
using ParadoxNotion.Design;
using UnityEditor;
using UnityEngine;

namespace AdventureGame.Editor.NodeCanvas
{

    /// <summary>
    /// Note to developers: Please describe what this class does.
    /// </summary>
    public class SoundInstanceDrawer : ObjectDrawer<SoundInstance>
    {
        public override SoundInstance OnGUI(GUIContent content, SoundInstance instance)
        {
            string value = instance?.Name;
            if (string.IsNullOrEmpty(value))
                value = "Null";

            GUI.enabled = false;
            EditorGUILayout.TextField(content, value);
            GUI.enabled = true;
            return instance;
        }
    }
}