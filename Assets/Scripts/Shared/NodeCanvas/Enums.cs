using System;

namespace AdventureGame.Shared.NodeCanvas
{
    public enum AxisDirection
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
        Both
    }

    public enum Direction {
        Up,
        Down,
        Left,
        Right
    }
    
    [Flags]
    public enum DirectionFilter
    {
        Up = 2,
        Right = 4,
        Down = 8,
        Left = 16
    }
}