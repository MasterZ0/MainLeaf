using Sirenix.OdinInspector;
using System;

namespace AdventureGame
{
    [IncludeMyAttributes, InlineProperty, HideLabel]
    [AttributeUsage(AttributeTargets.Field)]
    public class CustomBoxAttribute : Attribute
    {
        public bool Foldout;

        public CustomBoxAttribute(bool foldout = false)
        {
            Foldout = foldout;
        }
    }
}