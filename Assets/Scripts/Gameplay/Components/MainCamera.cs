using AdventureGame.Shared;
using UnityEngine;

namespace AdventureGame.Gameplay
{
    public class MainCamera : Singleton<MainCamera> 
    {
        [SerializeField] private Camera mainCamera;

        private void Reset() => TryGetComponent(out mainCamera);

        public static Vector3 Position => Instance.transform.position;
        public static Camera Camera => Instance.mainCamera;
    }
}