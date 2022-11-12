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

    public enum Axis2
    {
        X,
        Y
    }

    [Flags]
    public enum Axis2Flags
    {
        X = 1,
        Y = 2
    }

    public enum Axis3 
    {
        X,
        Y,
        Z
    }

    [Flags]
    public enum Axis3Flags
    {
        X = 1,
        Y = 2,
        Z = 4
    }
}