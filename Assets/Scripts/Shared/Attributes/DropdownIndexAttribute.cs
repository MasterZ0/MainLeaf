using System;

namespace AdventureGame.Shared 
{
    /// <summary>
    /// DropdownData is used on any property and creates a dropdown with configurable options.
    /// Use this to give the user a specific set of options to select from.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class DropdownIndexAttribute : Attribute
    {
        /// <summary> Beautiful display </summary>
        public bool UseNiceString = true;

        /// <summary> Expression used to convert the collection in string </summary>
        public string Expression = "x => x.ToString()";

        /// <summary> List of objects  </summary>
        public string Collection;

        /// <summary> Show button to reload the collection </summary>
        public bool ShowRefreshButton;

        /// <summary> Order the dropdown list </summary>
        public bool OrderByName;

        public DropdownIndexAttribute(string collection) 
        {
            Collection = collection;
        }
    }
}
