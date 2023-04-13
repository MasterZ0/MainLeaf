//using UnityEngine;
//using Z3.UIBuilder.Core.Editor;
//using Sirenix.Utilities.Editor;
//using UnityEditor;

//namespace AdventureGame.Audio
//{
//    public class SoundReferenceDrawer : OdinValueDrawer<SoundReference>
//    {
//        protected override void DrawPropertyLayout(GUIContent label)
//        {
//            SirenixEditorGUI.BeginBox();
//            {
//                EditorGUILayout.LabelField("♫ " + Property.NiceName);
//                CallNextDrawer(label);
//            }
//            SirenixEditorGUI.EndBox();

//        }
//    }
//}