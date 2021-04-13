using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PooledObject : MonoBehaviour {

    [Header("Pooled Object")]
    [SerializeField] private bool returnToQueue = true;

    public Queue<PooledObject> queuePool;
    protected abstract void StartObject();
    private bool returned;

    public void ActiveObject(Vector3 position, Quaternion rotation) // RECICLE OR SPAW
    {
        transform.position = position;
        transform.rotation = rotation;
        returned = false;
        gameObject.SetActive(true);
        StartObject();
    }

    public void ActiveObject() {
        gameObject.SetActive(true);
        StartObject();
    }

    public PooledObject SpawObject(Vector3 position, Quaternion rotation) { // Get object from pool
        PooledObject obj = ObjectPooler.Instance.SpawPooledObject(this);
        obj.ActiveObject(position, rotation);
        return obj;
    }

    public void ReturnToPool() {
        if (!returned) {
            returned = true;
            gameObject.SetActive(false);

            if (returnToQueue) {
                if (queuePool == null) {
                    queuePool = ObjectPooler.Instance.GetQueue(gameObject.name);
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
