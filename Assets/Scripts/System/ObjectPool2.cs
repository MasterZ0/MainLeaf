using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

/*
namespace Test {
    public class Player : MonoBehaviour {
        public Arrow arrowRef;
        public FireBall fireBallRef;

        void Spaw() {
            Arrow newArrow = arrowRef.SpawPooledObject();
            FireBall newFireBall = fireBallRef.SpawPooledObject();
        }
    }
    public class Arrow : PooledObject<Arrow> { }
    public class FireBall : PooledObject<FireBall> { }
    public abstract class PooledObject<T> : MonoBehaviour {

        public T SpawPooledObject(){
            T newObject = ObjectPool.Instance.GetPooledObject(this); 
            return newObject;
        }
    }
    public class ObjectPool : MonoBehaviour { 
        public static ObjectPool Instance;
        private Dictionary<PooledObject<T>, Type> objects;
        public T GetPooledObject<T>(T pooledObject) where T : PooledObject<T> {
            T newObject = Instantiate(pooledObject);
            objects.Add(newObject, pooledObject.GetType());
            return newObject;
        }
    }
}
*/

/*
namespace Test2 {
    public class Player : MonoBehaviour {
        public Arrow arrowRef;
        public FireBall fireBallRef;

        void Spaw() {
            Arrow newArrow = arrowRef.SpawPooledObject();
            FireBall newFireBall = fireBallRef.SpawPooledObject();
        }
    }
    public class Arrow : GenericPooled<Arrow> { }
    public class FireBall : GenericPooled<FireBall> { }

    public abstract class PooledObject : MonoBehaviour {

        public T SpawPooledObject<T>() where T : Object {
            T newObject = ObjectPool.Instance.GetPooledObject(this);
            return newObject;
        }
    }

    public abstract class GenericPooled<T> : PooledObject { }
    public class ObjectPool : MonoBehaviour {
        public static ObjectPool Instance;
        private Dictionary<PooledObject, Type> objects;
        public GenericPooled<T> GetPooledObject<T>(GenericPooled<T> pooledObject) {
            T newObject = Instantiate(pooledObject);
            objects.Add(newObject, pooledObject.GetType());
            return newObject;
        }
    }
}

*/