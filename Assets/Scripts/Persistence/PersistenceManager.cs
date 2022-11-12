using System.Collections.Generic;
using System;
using AdventureGame.Persistence.EncryptModules;
using AdventureGame.Persistence.StreamModules;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;

namespace AdventureGame.Persistence
{
    /// <summary>
    /// Handles data storage
    /// </summary>
    public static class PersistenceManager
    {
        /// <summary> Update all temporary datas </summary>
        public static event Action onBeforeSaveSlot = delegate { };

        private static readonly IPersistenceStreamModule PersistenceModule = new LocalStreamModule();
        private static readonly IEncryptModule EncryptModule = new NewtonsoftJsonEncryptModule();

        private static int CurrentSlot = -1;
        private const string GlobalPath = "Global";

        private static Dictionary<string, object> globalData;
        private static Dictionary<string, object> GlobalData
        {
            get
            {
                if (globalData == null)
                {
                    globalData = TryLoad(GlobalPath);
                }
                return globalData;
            }
        }

        private static Dictionary<string, object> temporaryData = new Dictionary<string, object>();

        public static string GetTypeKey(string roomName, GameObject gameObject) => $"{roomName}/{gameObject.name}";

        #region Volatile Data
        /// <summary>
        /// Save single data to volatile memory
        /// </summary>
        public static void Save<T>(T data) => Save(typeof(T).Name, data);

        /// <summary>
        /// Save data to volatile memory
        /// </summary>
        public static void Save<T>(string key, T data)
        {

            temporaryData[key] = data;
        }

        /// <summary>
        /// Load single data from volatile memory
        /// </summary>
        public static T Load<T>(T defaultValue) => Load(typeof(T).Name, defaultValue);

        /// <summary>
        /// Load data from volatile memory
        /// </summary>
        public static T Load<T>(string key, T defaultValue = default)
        {

            T loadedValue = defaultValue;

            if (temporaryData.ContainsKey(key))
            {
                return (T)temporaryData[key];
            }

            return loadedValue;
        }

        public static void NewSlot()
        {
            CurrentSlot = -1;
            temporaryData.Clear();
        }

        public static void ReloadCurrentSlot()
        {
            LoadSlot(CurrentSlot);
        }
        #endregion

        #region Slot Data
        /// <summary>
        /// Save the data in memory to a file referring to the slot
        /// </summary
        public static void SaveSlot(int slot)
        {
            CurrentSlot = slot;
            onBeforeSaveSlot();

            temporaryData = temporaryData.OrderBy(d => d.Key).ToDictionary(d => d.Key, d => d.Value);

            string encryptedData = EncryptModule.Encrypt(temporaryData);

            string slotPath = GetSlotPath(slot);
            PersistenceModule.WriteDataToKey(encryptedData, slotPath);
        }

        /// <summary>
        /// Read the file referring to the slot and load it into volatile memory
        /// </summary>
        public static void LoadSlot(int slot)
        {
            CurrentSlot = slot;
            string path = GetSlotPath(slot);
            temporaryData = TryLoad(path);
        }

        /// <summary>
        /// Load single data directly from the slot
        /// </summary>
        public static T LoadDataFromSlot<T>(int slot, T defaultValue = default) => LoadDataFromSlot(typeof(T).Name, slot, defaultValue);

        /// <summary>
        /// Load data directly from the slot
        /// </summary>
        public static T LoadDataFromSlot<T>(string key, int slot, T defaultValue)
        {
            string path = GetSlotPath(slot);
            Dictionary<string, object> loadedData = TryLoad(path);

            if (loadedData.ContainsKey(key))
            {
                return (T)loadedData[key];
            }
            return defaultValue;
        }

        public static void ClearSlot(int slot)
        {

            string slotPath = GetSlotPath(slot);
            PersistenceModule.ClearDataFromKey(slotPath);
        }
        #endregion

        #region Global Data
        public static void SaveGlobalFile<T>(T data)
        {
            string key = typeof(T).Name;
            GlobalData[key] = data;

            SaveAllGlobalFiles();
        }

        public static void SaveAllGlobalFiles()
        {
            string encryptedData = EncryptModule.Encrypt(GlobalData);
            PersistenceModule.WriteDataToKey(encryptedData, GlobalPath);
        }

        public static bool ContainsGlobalFile<T>()
        {
            string key = typeof(T).Name;
            return GlobalData.ContainsKey(key);
        }

        public static T LoadGlobalFile<T>(T defaultValue = default)
        {
            string key = typeof(T).Name;
            if (GlobalData.ContainsKey(key))
            {
                return (T)globalData[key];
            }

            return defaultValue;
        }
        #endregion

        #region Private Methods
        private static Dictionary<string, object> TryLoad(string slotPath)
        {
            string encryptedData = PersistenceModule.LoadDataFromKey(slotPath);

            // No data
            if (string.IsNullOrEmpty(encryptedData))
            {
                return new Dictionary<string, object>();
            }

            try
            {
                return EncryptModule.Decrypt<Dictionary<string, object>>(encryptedData);
            }
            catch (Exception e)
            {
                Debug.LogError($"The Path '{slotPath}' was corrupted and is being deleted \nMessage: {e.Message}");
                PersistenceModule.ClearDataFromKey(slotPath);
                return new Dictionary<string, object>();
            }
        }

        private static string GetSlotPath(int slot)
        {
            return $"SaveSlot{slot}";
        }
        #endregion
    }
}