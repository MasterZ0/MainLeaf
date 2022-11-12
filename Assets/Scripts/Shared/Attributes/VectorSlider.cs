using System;
using UnityEngine;

namespace AdventureGame
{
    /// <summary>
    /// Note to developers: Please describe what this class does.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class VectorSlider : Attribute
    {
        public Vector2 Range { get; }
        public VectorSlider (float min, float max) {
            Range = new Vector2(min, max);
        }
    }
}