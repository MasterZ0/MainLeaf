using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace AdventureGame.Editor
{
    public static class AddressablesEditorUtils
    {
        private const AddressableAssetSettings.ModificationEvent ModificationEvent = AddressableAssetSettings.ModificationEvent.EntryMoved;

        public static bool IsAddressable(Object asset, out string address)
        {
            address = "";
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
            AddressableAssetEntry entry = settings.FindAssetEntry(GetAssetGuid(asset));

            if (entry != null)
                address = entry.address;
            
            return entry != null;
        }
        
        public static void SetupAsset(Object asset, string address, string groupName, string label)
        {
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
            AddressableAssetGroup roomsGroup = settings.FindGroup(groupName);
            
            string assetGuid = GetAssetGuid(asset);
            AddressableAssetEntry assetEntry = settings.CreateOrMoveEntry(assetGuid, roomsGroup);
            assetEntry.address = address;
            assetEntry.labels.Add(label);

            settings.SetDirty(ModificationEvent, assetEntry, true);
            AssetDatabase.SaveAssets();
        }
        
        public static void UpdateAsset(Object asset, string address, string groupName, string label)
        {
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
            AddressableAssetGroup roomsGroup = settings.FindGroup(groupName);
            
            string assetGuid = GetAssetGuid(asset);
            AddressableAssetEntry assetEntry = settings.FindAssetEntry(assetGuid, roomsGroup);
            
            if (assetEntry == null)
                SetupAsset(asset, address, groupName, label);
            
            assetEntry.address = address;
            assetEntry.labels.Clear();
            assetEntry.labels.Add(label);

            settings.SetDirty(ModificationEvent, assetEntry, true);
            AssetDatabase.SaveAssets();
        }

        private static string GetAssetGuid(Object asset)
        {
            string assetPath = AssetDatabase.GetAssetPath(asset);
            return AssetDatabase.AssetPathToGUID(assetPath);
        }
    }
}