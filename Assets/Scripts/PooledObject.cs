using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PooledObject : MonoBehaviour
{
    [Header("Pooled Object")]
    [SerializeField]
    private bool returnToQueue = true;

    internal Queue<PooledObject> queuePool;
    protected abstract void StartObject();
    private bool returned;

    internal void ActiveObject(Vector3 position, Quaternion rotation) // RECICLE OR SPAW
    {
        transform.position = position;
        transform.rotation = rotation;
        returned = false;
        gameObject.SetActive(true);
        StartObject();
    }

    internal void ActiveObject() {
        gameObject.SetActive(true);
        StartObject();
    }

    internal PooledObject SpawObject(Vector3 position, Quaternion rotation) // FIND OBJECT POOL
    {
        PooledObject obj = ObjectPooler.instance.SpawPooledObject(this);
        obj.ActiveObject(position, rotation);
        return obj;
    }

    internal void ReturnToPool() {
        if (!returned) {
            returned = true;
            gameObject.SetActive(false);

            if (returnToQueue) {
                if (queuePool == null) {
                    queuePool = ObjectPooler.instance.GetQueue(gameObject.name);
                }
                queuePool.Enqueue(this);
            }
        }
    }

    public void OnReloadScene() {
        if (returnToQueue && gameObject.activeSelf)
            ReturnToPool();
    }
}
