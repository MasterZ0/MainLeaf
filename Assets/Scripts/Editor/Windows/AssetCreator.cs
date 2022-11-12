using UnityEditor;
using UnityEngine;
using System.Reflection;
using System;
using AdventureGame.Shared;
using Sirenix.OdinInspector;
using System.Linq;

namespace AdventureGame.Editor
{
    [Serializable]
    public class AssetCreator<T> where T : ScriptableObject
    {
        [TitleGroup("File Settings"), HorizontalGroup("File Settings/Main")]
        [SerializeField] private string fileName;
        [SerializeField] private string subFolder;
        [DropdownIndex(nameof(Types), Expression = "x => x.Name"), OnValueChanged(nameof(ValidateSubFolder))]
        [SerializeField] private int selectedType;

        [ShowInInspector]
        private string AssetPath => $"{Path}/{subFolder}/{fileName}.asset";

        private Type[] Types { get; }
        private string Path { get; }
        private bool Addressable { get; }

        /// <param name="path"> Path inside the project. Ex: Assets/Data </param>
        public AssetCreator(string path, bool addressable = false)
        {
            // Preferences
            Path = path;
            Addressable = addressable;

            // Get all subtypes
            Type assetType = typeof(T);
            Types = Assembly.GetAssembly(assetType)
                            .GetTypes()
                            .Where(t => t.IsClass && !t.IsAbstract && (t.IsSubclassOf(assetType) || t == assetType))
                            .ToArray();

            // Default values
            fileName = "New" + typeof(T).Name;
            ValidateSubFolder();
        }

        /// <summary> Try to give a simplified subFolder </summary>
        private void ValidateSubFolder() => subFolder = Types[selectedType].Name.Replace(typeof(T).Name, string.Empty);
        
        [HorizontalGroup("File Settings/Main"), Button]
        private void Create()
        {
            Type type = Types[selectedType];

            T itemData = ScriptableObject.CreateInstance(type) as T;
            AssetDatabase.CreateAsset(itemData, AssetPath);

            if (Addressable)
            {
                T createdItem = AssetDatabase.LoadAssetAtPath<T>(AssetPath);
                string address = $"Items_{createdItem.name}";
                AddressablesEditorUtils.SetupAsset(createdItem, address, "Items", "Item");
                AssetDatabase.SaveAssets();
            }
        }
    }
}