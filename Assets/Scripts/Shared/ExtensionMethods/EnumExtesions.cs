using AdventureGame.Shared.NodeCanvas;
using System;
using UnityEngine;

namespace AdventureGame.Shared.ExtensionMethods
{
    public static class EnumExtesions
    {
        public static float Distance(this Axis3Flags axis, Vector3 a, Vector3 b)
        {
            float squaredDifference = 0;

            if (axis.HasFlag(Axis3Flags.X))
            {
                float aux = a.x - b.x;
                squaredDifference += aux * aux;
            }
            if (axis.HasFlag(Axis3Flags.Y))
            {
                float aux = a.y - b.y;
                squaredDifference += aux * aux;

            }
            if (axis.HasFlag(Axis3Flags.Z))
            {
                float aux = a.z - b.z;
                squaredDifference += aux * aux;
            }

            return Mathf.Sqrt(squaredDifference);
        }
    }
}