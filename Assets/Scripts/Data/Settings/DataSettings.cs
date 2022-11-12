using System;
using UnityEngine;

namespace AdventureGame.Data
{
    [Serializable]
    public class DataSettings<T>
    {
        [SerializeField] private T develop;
        [SerializeField] private T staging;
        [SerializeField] private T release;

        public static implicit operator T(DataSettings<T> data)
        {
            return GameSettings.Environment switch
            {
                EnvironmentState.Develop => data.develop,
                EnvironmentState.Staging => data.staging,
                EnvironmentState.Release => data.release,
                _ => throw new NotImplementedException(),
            };
        }
    }
}
