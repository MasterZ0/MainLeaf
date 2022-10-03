using System;
using UnityEngine;

namespace AdventureGame.Shared {

    /// <summary>
    /// TypeSelectionAttribute is used on any field and creates a drop-down list with configurable options.
    /// Use this in a collection of a base type to give the user a specific set of options from the abstractions of a class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class TypeSelectionAttribute : Attribute {

        public TextAlignment TextAlignment { get; }
        public bool HorizontalLine { get; }
        public bool BoldLabel { get; }

        public TypeSelectionAttribute(TextAlignment textAlignment = TextAlignment.Left, bool horizontalLine = true, bool boldLabel = true) {
            TextAlignment = textAlignment;
            HorizontalLine = horizontalLine;
            BoldLabel = boldLabel;
        }
    }
}
