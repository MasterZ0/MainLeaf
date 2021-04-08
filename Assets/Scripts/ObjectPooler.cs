using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public Transform shortPool;
    public Dictionary<string, Queue<PooledObject>> poolDictionary;

    public Action reloadAction;
    public static ObjectPooler Instance { get; private set; }

    private void Awake() {
        Instance = this;
        poolDictionary = new Dictionary<string, Queue<PooledObject>>();
    }

    public void Clear(bool reloadScene) {
        if (reloadScene && reloadAction != null) {
            reloadAction();
            return;
        }

        reloadAction = null;
        poolDictionary = new Dictionary<string, Queue<PooledObject>>();

        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        //foreach (Transform child in shortPool) {
        //    Destroy(child.gameObject);
        //}
    }

    public PooledObject SpawPooledObject(PooledObject pooledObject) {
        Queue<PooledObject> queue = GetQueue(pooledObject.name);
        return GetObject(queue, pooledObject);
    }


    public Queue<PooledObject> GetQueue(string objectName) {
        if (poolDictionary.ContainsKey(objectName))
            return poolDictionary[objectName];

        Queue<PooledObject> objectPool = new Queue<PooledObject>();
        poolDictionary.Add(objectName, objectPool);
        return objectPool;
    }


    private PooledObject GetObject(Queue<PooledObject> pool, PooledObject prefab) {
        if (pool.Count > 0)
            return pool.Dequeue();

        PooledObject newObj = Instantiate(prefab, transform);
        newObj.gameObject.SetActive(false);
        newObj.queuePool = pool;                // Ele sabe o caminho de casa
        reloadAction += newObj.OnReloadScene;   // Se inscreveu no reload
        return newObj;
    }
}
