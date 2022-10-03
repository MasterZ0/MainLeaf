using UnityEngine;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;

namespace AdventureGame.Audio
{
    public class SoundReferenceDrawer : OdinValueDrawer<SoundReference>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            SirenixEditorGUI.BeginBox();
            {
                EditorGUILayout.LabelField("♫ " + Property.NiceName);
                CallNextDrawer(label);
            }
            SirenixEditorGUI.EndBox();

        }
    }
}