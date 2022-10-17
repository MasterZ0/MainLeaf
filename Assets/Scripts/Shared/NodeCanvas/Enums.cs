using System;

namespace AdventureGame.Shared.NodeCanvas
{
    public enum Direction
    {
        Left,
        Right,
        Up,
        Down,
        Forward,
        Back
    }

    public enum Axis {
        X,
        Y,
        Z
    }

    [Flags]
    public enum AxisFlags
    {
        X = 1,
        Y = 2,
        Z = 4
    }
}