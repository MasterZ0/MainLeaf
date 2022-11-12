using System;
using UnityEngine;

namespace AdventureGame.Shake
{
    /// <summary>
    /// Receive the shake parameters and send them to the listeners
    /// </summary>
    public class Shaker : MonoBehaviour
    {
        public static event Action<ShakeResult> OnUpdateShake = delegate { };

        private static Shaker instance;
        public static Shaker Instance
        {
            get
            {
                CreateInstance();
                return instance;
            }
        }

        private static bool shakeActive = true;

        private ShakeInstance currentShake;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void CreateInstance()
        {
            if (instance != null)
                return;

            instance = new GameObject("[Shaker]").AddComponent<Shaker>();
            DontDestroyOnLoad(instance);
        }

        public static void SetActiveShake(bool enable) => shakeActive = enable;
        public static void RequestShake(ShakeParameters shake) => Instance.OnShake(shake);

        private void OnShake(ShakeParameters shake)
        {
            if (!shakeActive || (currentShake != null && currentShake.ShakeParameters.SortOrder > shake.SortOrder))            
                return;
            
            currentShake = new ShakeInstance(shake);
        }

        private void Update()
        {
            if (currentShake == null)
                return;

            ShakeResult shake = currentShake.UpdateShake(Time.deltaTime);

            if (currentShake.IsFinished)
            {
                currentShake = null;
            }

            OnUpdateShake(shake);
        }
    }
}