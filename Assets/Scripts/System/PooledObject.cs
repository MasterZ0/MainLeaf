using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Opções, ou o objeto já está instanciado na cena, ou ele será instanciado
public abstract class PooledObject : MonoBehaviour {

    [Header("Pooled Object")]
    [SerializeField] private bool pooledQueue = true;
    public bool PooledQueue { get => pooledQueue; set => pooledQueue = value; }

    public Queue<PooledObject> QueuePool { get; set; }
    protected abstract void OnEnablePooledObject();

    protected virtual void Start() {
        ObjectPooler.Instance.OnReloadScene += OnReloadScene;
    }

    protected virtual void OnEnable() {
        if(pooledQueue)
            OnEnablePooledObject();
    }
    protected virtual void OnDisable() {
        ReturnToPool();
    }

    public void ActiveObject(Vector3 position, Quaternion rotation) // RECICLE OR SPAW
    {
        transform.position = position;
        transform.rotation = rotation;
        gameObject.SetActive(true);
    }

    public void ActiveObject() {
        gameObject.SetActive(true);
        OnEnablePooledObject();
    }

    public PooledObject SpawObject(Vector3 position, Quaternion rotation) { // Get object from pool
        PooledObject obj = ObjectPooler.Instance.SpawPooledObject(this);
        obj.ActiveObject(position, rotation);
        return obj;
    }

    public void DesactivePooledObject() {
        gameObject.SetActive(false);
    }

    private void ReturnToPool() {
        if (pooledQueue) {
            if (QueuePool == null) {
                QueuePool = ObjectPooler.Instance.GetQueue(gameObject.name);
            }
            QueuePool.Enqueue(this);
        }
    }

    private void OnReloadScene() {
        if (pooledQueue && gameObject.activeSelf)
            ReturnToPool();
    }
}
