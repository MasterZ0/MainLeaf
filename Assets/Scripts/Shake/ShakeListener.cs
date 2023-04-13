using Cinemachine;
using Z3.UIBuilder.Core;
using UnityEngine;

namespace AdventureGame.Shake
{
    /// <summary>
    /// Apply shake in the associeted virtual camera
    /// </summary>
    public class ShakeListener : MonoBehaviour
    {
        [Title("Shake Listenet")]
        [SerializeField] private CinemachineVirtualCamera cinemachine;

        private CinemachineBasicMultiChannelPerlin basic;

        private void Reset()
        {
            if (cinemachine == null)
                TryGetComponent(out cinemachine);
        }

        private void Awake()
        {
            basic = cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        private void OnEnable()
        {
            basic.m_NoiseProfile = null;
            Shaker.OnUpdateShake += OnUpdateShake;
        }

        private void OnDisable()
        {
            Shaker.OnUpdateShake -= OnUpdateShake;
        }

        public void OnUpdateShake(ShakeResult result)
        {
            basic.m_NoiseProfile = result;
            basic.m_AmplitudeGain = result.power;
            basic.m_FrequencyGain = result.power;
        }
    }
}