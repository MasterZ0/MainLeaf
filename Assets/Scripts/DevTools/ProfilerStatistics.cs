using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Unity.Profiling;
using System.Text;
using Sirenix.OdinInspector;

namespace AdventureGame.DevTools
{
    /// <summary>
    /// Tutorial: https://resources.unity.com/unitenow/onlinesessions/capturing-profiler-stats-at-runtime
    /// Doc: https://docs.unity3d.com/2020.2/Documentation/ScriptReference/Unity.Profiling.ProfilerRecorder.html
    /// Statistics list: https://docs.unity3d.com/2020.2/Documentation/Manual/ProfilerMemory.html
    /// Draw call / Batches : https://docs.unity3d.com/Manual/DrawCallBatching.html
    /// </summary>
    [System.Serializable]
    public class ProfilerStatistics
    {
        [Title("Profiler Statistics")]
        [Range(0.01f, 2f)]
        [SerializeField] private float updateRate = 0.1f;
        [SerializeField] private GameObject panel;
        [SerializeField] private TextMeshProUGUI debugText;

        private ProfilerRecorder mainThreadTimeRecorder;
        private ProfilerRecorder gcMemoryRecorder;
        private ProfilerRecorder systemMemoryRecorder;
        private ProfilerRecorder drawCallsCountRecorder;

        private float deltaTime;
        private float updateCounter;

        private bool active;

        private const int MBScale = 1024 * 1024;
        private const string MainThread = "Main Thread";
        private const string SystemUsedMemory = "System Used Memory";
        private const string GCReservedMemory = "GC Reserved Memory";
        private const string DrawCallsCount = "Draw Calls Count";

        public void Init(bool active = false)
        {
            if (active)
            {
                Enable();
            }
            else
            {
                panel.SetActive(false);
            }
        }

        public void ToogleActivation()
        {
            if (!active)
            {
                Enable();
            }
            else
            {
                Disable();
            }
        }

        public void Enable()
        {
            active = true;
            panel.SetActive(true);

            mainThreadTimeRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Internal, MainThread, 15);
            systemMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, SystemUsedMemory);
            gcMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, GCReservedMemory);
            drawCallsCountRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, DrawCallsCount);
        }

        public void Disable()
        {
            active = false;
            panel.SetActive(false);

            mainThreadTimeRecorder.Dispose();
            gcMemoryRecorder.Dispose();
            systemMemoryRecorder.Dispose();
            drawCallsCountRecorder.Dispose();
        }
        
        public void Update()
        {
            updateCounter += Time.unscaledDeltaTime;
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

            if (!active)
                return;

            if (updateCounter >= updateRate)
            {
                updateCounter -= updateRate;

                // Update Text
                float fps = 1.0f / deltaTime;
                double ms = GetRecorderFrameAverage(mainThreadTimeRecorder) * 1e-6f;

                StringBuilder stringBuilder = new StringBuilder(500);
                stringBuilder.AppendLine($"Graphics: {fps:F1} FPS ({ms:F1} ms)");
                stringBuilder.AppendLine($"Memory -> GC: {gcMemoryRecorder.LastValue / MBScale} MB, System: {systemMemoryRecorder.LastValue / MBScale} MB");
                stringBuilder.AppendLine($"Draw Calls Count: {drawCallsCountRecorder.LastValue}");
                debugText.text = stringBuilder.ToString();
            }
        }

        public static double GetRecorderFrameAverage(ProfilerRecorder recorder)
        {
            int samplesCount = recorder.Capacity;
            if (samplesCount == 0)
                return 0;

            double rate = 0;
            List<ProfilerRecorderSample> samples = new List<ProfilerRecorderSample>(samplesCount);
            recorder.CopyTo(samples);
            for (int i = 0; i < samples.Count; i++)
            {
                rate += samples[i].Value;
            }

            return rate / samplesCount;
        }
    }
}
