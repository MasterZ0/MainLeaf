using AdventureGame.Shared.NodeCanvas;
using System;
using UnityEngine;

namespace AdventureGame.Shared.ExtensionMethods
{
    public static class EnumExtesions
    {
        public static float Distance(this AxisFlags axis, Vector3 a, Vector3 b)
        {
            float squaredDifference = 0;

            if (axis.HasFlag(AxisFlags.X))
            {
                float aux = a.x - b.x;
                squaredDifference += aux * aux;
            }
            if (axis.HasFlag(AxisFlags.Y))
            {
                float aux = a.y - b.y;
                squaredDifference += aux * aux;

            }
            if (axis.HasFlag(AxisFlags.Z))
            {
                float aux = a.z - b.z;
                squaredDifference += aux * aux;
            }

            return Mathf.Sqrt(squaredDifference);
        }
    }
}