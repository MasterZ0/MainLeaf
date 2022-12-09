using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace AdventureGame.Editor
{
    public static class AddressablesEditorUtils
    {
        private const AddressableAssetSettings.ModificationEvent ModificationEvent = AddressableAssetSettings.ModificationEvent.EntryMoved;

        private static AddressableAssetSettings Settings => AddressableAssetSettingsDefaultObject.Settings;

        /// <summary> Check if the Addressable box is checked on the asset </summary>
        public static bool IsAddressable(Object asset)
        {
            string assetGuid = GetAssetGuid(asset);
            AddressableAssetEntry entry = Settings.FindAssetEntry(assetGuid);
            return entry != null;
        }

        /// <summary> Check if the Addressable box is checked on the asset </summary>
        public static void SetupAsset(Object asset, string groupName, string address, List<string> labels)
        {
            AddressableAssetGroup roomsGroup = Settings.FindGroup(groupName);

            string assetGuid = GetAssetGuid(asset);
            AddressableAssetEntry assetEntry = Settings.CreateOrMoveEntry(assetGuid, roomsGroup);
            assetEntry.address = address;

            labels.ForEach(l => assetEntry.labels.Add(l));

            Settings.SetDirty(ModificationEvent, assetEntry, true);
            AssetDatabase.SaveAssets();

            Debug.Log($"Added asset '{asset.name}' with the address '{address}'", asset);
        }

        /// <summary> Valitade address and labels </summary>
        public static void UpdateAsset(Object asset, string groupName, string address, List<string> labels)
        {
            AddressableAssetGroup roomsGroup = Settings.FindGroup(groupName);

            string assetGuid = GetAssetGuid(asset);
            AddressableAssetEntry assetEntry = Settings.FindAssetEntry(assetGuid, roomsGroup);

            if (assetEntry.address != address || assetEntry.labels.ToList() != labels)
            {
                assetEntry.address = address;
                assetEntry.labels.Clear();
                labels.ForEach(l => assetEntry.labels.Add(l));

                Settings.SetDirty(ModificationEvent, assetEntry, true);
                AssetDatabase.SaveAssets();

                Debug.Log($"Fixed asset '{asset.name}' with the address '{address}'", asset);
            }
        }

        /// <summary>  Useful to find AddressableAssetEntry </summary>
        private static string GetAssetGuid(Object asset)
        {
            string assetPath = AssetDatabase.GetAssetPath(asset);
            return AssetDatabase.AssetPathToGUID(assetPath);
        }
    }
}