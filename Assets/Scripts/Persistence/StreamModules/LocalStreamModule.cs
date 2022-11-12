using System.IO;
using System.Text;
using UnityEngine;

namespace AdventureGame.Persistence.StreamModules
{
    public class LocalStreamModule : IPersistenceStreamModule
    {
        private static readonly Encoding Encoding = Encoding.Unicode;
        
        public string LoadDataFromKey(string key) {
            
            string path = GetPathFromKey(key);
            return !File.Exists(path)
                ? string.Empty
                : File.ReadAllText(path, Encoding);
        }

        public void WriteDataToKey(string encryptedData, string key) {
            
            string path = GetPathFromKey(key);
            FileStream saveStream = new FileStream(path, FileMode.Create);
            
            byte[] bytes = EncodeText(encryptedData);
            saveStream.Write(bytes, 0, bytes.Length);
            saveStream.Close();
        }
        
        public void ClearDataFromKey(string key) {
            
            string path = GetPathFromKey(key);

            if (File.Exists(path))
                File.Delete(path);
        }

        private static byte[] EncodeText(string jsonData) {
            
            byte[] bytes = Encoding.GetBytes(jsonData);
            return bytes;
        }

        private static string GetPathFromKey(string key) {
        
            string persistentDataPath = Application.persistentDataPath;
            string path = Path.Combine(persistentDataPath, $"{key}.adventuregame");
            return path;
        }
    }
}