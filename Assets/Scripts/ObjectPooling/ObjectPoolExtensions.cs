using UnityEngine;

namespace AdventureGame.ObjectPooling 
{
    public static class ObjectPoolExtensions 
    {

        public static T SpawnPooledObject<T>(this T pooledObject, Vector2 position = default, Quaternion rotation = default, Transform parent = null) where T : Component 
        {
            return ObjectPool.SpawnPooledObject(pooledObject, position, rotation, parent);
        }

        public static T SpawnPooledObject<T>(this T pooledObject, Transform parent = null) where T : Component
        {
            return ObjectPool.SpawnPooledObject(pooledObject, parent);
        }

        public static void ReturnToPool<T>(this T pooledObject) where T : Component 
        {
            ObjectPool.ReturnToPool(pooledObject);
        }
    }
}
