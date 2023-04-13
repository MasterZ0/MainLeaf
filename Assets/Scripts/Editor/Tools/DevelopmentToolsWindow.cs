//using UnityEngine;
//using UnityEditor;
//using Z3.UIBuilder.Core.Editor;
//using Sirenix.Utilities.Editor;
//using Z3.UIBuilder.Core;
//using AdventureGame.Shared;

//namespace AdventureGame.Editor
//{
//    /// <summary>
//    /// Customized tools to help with Game Design and Development
//    /// </summary>
//    public class DevelopmentToolsWindow : OdinEditorWindow
//    {
//        [TabGroup("Level Design Tools")]
//        public LevelDesignTools levelDesignTools = new LevelDesignTools();

//        [TabGroup("Repair Tools")]
//        public RepairTools repairTools = new RepairTools();

//        private const string DevelopmentTools = "Development Tools";

//        [MenuItem(ProjectPath.ApplicationName + "/" + DevelopmentTools)]
//        public static void ShowWindow()
//        {
//            GetWindow<DevelopmentToolsWindow>(DevelopmentTools).Show();
//        }

//        protected override void DrawEditors()
//        {
//            SirenixEditorGUI.Title(DevelopmentTools, string.Empty, TextAlignment.Center, true);

//            base.DrawEditors();
//        }
//    }
//}