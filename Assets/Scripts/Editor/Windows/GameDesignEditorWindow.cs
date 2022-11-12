using AdventureGame.Data;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using AdventureGame.Shared.ExtensionMethods;
using System.Reflection;
using System;
using AdventureGame.Shared;
using Sirenix.OdinInspector;
using AdventureGame.Items.Data;

namespace AdventureGame.Editor
{
    /// <summary>
    /// Create the Game Design screen
    /// </summary>
    public partial class GameDesignEditorWindow : OdinMenuEditorWindow
    {
        private AssetCreator<ItemData> itemCreator;

        // Copy and past
        private ScriptableObject memoryObject;
        private ScriptableObject currentAsset;

        private string assetName;

        private const string Title = "Are you sure about that?";
        private const string Message = "You are about to change all assets in this development environment and all data will be permanently lost";
        private const string Ok = "Confirm";
        private const string Cancel = "Cancel";

        private const string CopyValues = "Copy values";
        private const string PasteValues = "Paste values";
        private const string Delete = "Delete";

        public const string ApplicationName = nameof(AdventureGame);

        [MenuItem(ApplicationName + "/Game Design")]
        private static void OpenMenu()
        {
            GetWindow<GameDesignEditorWindow>($"{ApplicationName} Settings").Show();
        }

        #region Menu Tree
        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree tree = new OdinMenuTree();
            GameSettings.OnChangeEnvironment += RebuildTree;

            DrawTree(tree);
            SetMenuTreeColor(tree);
            return tree;
        }

        private void RebuildTree()
        {
            GameSettings.OnChangeEnvironment -= RebuildTree;
            ForceMenuTreeRebuild();
        }

        private void DrawTree(OdinMenuTree tree)
        {
            string settingsPath = $"{ApplicationName} Settings";

            // GameValues
            GameSettings simulatorValues = AssetDatabase.LoadAssetAtPath<GameSettings>(ProjectPath.GameSettingsAsset);
            tree.Add(settingsPath, simulatorValues, GetEnvironmentIcon());

            // Environmments
            tree.Add($"{settingsPath}/Arena", GameSettings.Arena, GetIcon(SdfIconType.Bank));
            tree.Add($"{settingsPath}/General", GameSettings.General, EditorIcons.SettingsCog);
            tree.Add($"{settingsPath}/UI", GameSettings.UI, EditorIcons.ImageCollection);

            tree.Add($"{settingsPath}/Players", null, EditorIcons.SingleUser);
            tree.AddAllAssetsAtPath($"{settingsPath}/Players", $"{ProjectPath.GameSettingsPath}/{GameSettings.Environment}/Players", typeof(ScriptableObject), true);

            tree.Add($"{settingsPath}/Enemies", null, EditorIcons.PacmanGhost);
            tree.AddAllAssetsAtPath($"{settingsPath}/Enemies", $"{ProjectPath.GameSettingsPath}/{GameSettings.Environment}/Enemies", typeof(ScriptableObject), true);

            // Items
            itemCreator = new AssetCreator<ItemData>($"{ProjectPath.ItemsPath}");
            tree.Add("Items", itemCreator, EditorIcons.GridBlocks);
            tree.AddAllAssetsAtPath("Items", ProjectPath.ItemsPath, typeof(ItemData), true);
            tree.EnumerateTree().AddIcons<ItemData>(x => x.icon);

            // Beauty display
            tree.EnumerateTree(x =>
            {
                x.Name = x.Name.StringReduction().GetNiceString();
            });

            // Sort
            tree.EnumerateTree().SortMenuItemsByName();
        }

        private Texture2D GetIcon(SdfIconType iconType) => SdfIcons.CreateTransparentIconTexture(iconType, Color.white, 16 ,16, 0);
        #endregion

        #region Tree Customization
        private EditorIcon GetEnvironmentIcon() => GameSettings.Environment switch
        {
            EnvironmentState.Develop => EditorIcons.Char2,
            EnvironmentState.Staging => EditorIcons.Char3,
            EnvironmentState.Release => EditorIcons.Char1,
            _ => throw new ArgumentOutOfRangeException(),
        };

        private Color GetEnvironmentColor() => GameSettings.Environment switch
        {
            EnvironmentState.Develop => new Color(1f, 0.239f, 0.407f),
            EnvironmentState.Release => new Color(0, 0.6f, 0.6f),
            EnvironmentState.Staging => new Color(0.654f, 0.203f, 0.537f),
            _ => throw new NotImplementedException(),
        };

        private void SetMenuTreeColor(OdinMenuTree menuTree)
        {
            Color color = GetEnvironmentColor();

            menuTree.Config = new OdinMenuTreeDrawingConfig
            {
                DefaultMenuStyle =
                {
                    SelectedColorDarkSkin = color,
                    SelectedColorLightSkin = color
                }
            };
        }
        #endregion

        #region Toolbar
        protected override void OnBeginDrawEditors()
        {
            ScriptableObject asset = MenuTree?.Selection.SelectedValue as ScriptableObject;

            if (asset == null)
                return;

            if (asset != currentAsset)
            {
                currentAsset = asset;
                assetName = currentAsset.name;
            }

            SirenixEditorGUI.BeginHorizontalToolbar();
            {
                if (currentAsset is GameSettings)
                {
                    GUILayout.FlexibleSpace(); // Move ping to right
                }
                else // If have more types, create a forbidden types method
                {
                    if (currentAsset is IEditableAsset)
                    {
                        DrawDelete(currentAsset);
                    }

                    GUILayout.FlexibleSpace();
                    DrawCopyAndPast(currentAsset);
                }

                if (SirenixEditorGUI.ToolbarButton(EditorIcons.LightBulb))
                {
                    EditorGUIUtility.PingObject(currentAsset);
                }
            }
            SirenixEditorGUI.EndHorizontalToolbar();

            if (currentAsset is IEditableAsset)
            {
                DrawAssetName();
            }
        }

        private void DrawAssetName()
        {
            SirenixEditorGUI.BeginHorizontalToolbar();
            {                
                assetName = EditorGUILayout.TextField("Asset Name", assetName);

                GUI.enabled = assetName != currentAsset.name;
                if (SirenixEditorGUI.ToolbarButton("Update Asset Name"))
                {
                    string path = AssetDatabase.GetAssetPath(currentAsset);
                    AssetDatabase.RenameAsset(path, assetName);
                }
                GUI.enabled = true;
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }
        private void DrawDelete(ScriptableObject asset)
        {
            if (SirenixEditorGUI.ToolbarButton(Delete))
            {
                string path = AssetDatabase.GetAssetPath(asset);
                AssetDatabase.DeleteAsset(path);
            }
        }

        private void DrawCopyAndPast(ScriptableObject asset)
        {
            if (SirenixEditorGUI.ToolbarButton(CopyValues))
            {
                memoryObject = asset;
            }

            GUI.enabled = memoryObject != null && memoryObject.GetType() == asset.GetType();

            if (SirenixEditorGUI.ToolbarButton(PasteValues))
            {
                EditorUtility.CopySerializedManagedFieldsOnly(memoryObject, asset);
                EditorUtility.SetDirty(asset);
                AssetDatabase.SaveAssets();
            }
            GUI.enabled = true;
        }

        private void CopyAndPasteFields(object current, object toCopy)
        {
            FieldInfo[] fieldsFromCurrent = current.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (FieldInfo field in fieldsFromCurrent)
            {
                ScriptableObject objFromCurrent = field.GetValue(current) as ScriptableObject;
                ScriptableObject objFromtoCopy = field.GetValue(toCopy) as ScriptableObject;

                if (objFromCurrent != null && objFromtoCopy != null)
                {
                    EditorUtility.CopySerializedManagedFieldsOnly(objFromtoCopy, objFromCurrent);
                    EditorUtility.SetDirty(objFromCurrent);
                }
            }
        }
        #endregion
    }
}