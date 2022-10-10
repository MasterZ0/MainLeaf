using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AdventureGame.Player
{
    public class FacilitatorBuffer
    {
        private readonly Dictionary<string, float> keysBuffer = new Dictionary<string, float>();

        public void SendToBuffer(string key, float time)
        {
            keysBuffer[key] = time;
        }

        public bool HasKey(string key) => keysBuffer.ContainsKey(key);

        public void RemoveKey(string key)
        {
            if (keysBuffer.ContainsKey(key))
                keysBuffer.Remove(key);
        }

        public void UpdateBuffers()
        {
            if (keysBuffer.Count == 0)
                return;

            float time = Time.fixedDeltaTime;
            foreach (string key in keysBuffer.Keys.ToList())
            {
                keysBuffer[key] -= time;

                if (keysBuffer[key] <= 0f)
                {
                    keysBuffer.Remove(key);
                }
            }
        }
    }
}