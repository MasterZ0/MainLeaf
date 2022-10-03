using UnityEngine;

namespace AdventureGame.Persistence.EncryptModules
{
    public class UnityJsonEncryptModule : IEncryptModule 
    {
        public string Encrypt<T>(T data)
        {
            return JsonUtility.ToJson(data);
        }

        public T Decrypt<T>(string data)
        {
            return JsonUtility.FromJson<T>(data);
        }
    }
}