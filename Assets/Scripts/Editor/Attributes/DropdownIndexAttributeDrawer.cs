using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities.Editor;
using System.Collections.Generic;
using System.Collections;
using UnityEditor;
using UnityEngine;
using AdventureGame.Shared;
using System.Linq;
using AdventureGame.Shared.ExtensionMethods;
using System.Linq.Dynamic.Core;
using System.Data;
using System;

namespace AdventureGame.Editor
{
    public class DropdownIndexAttributeDrawer : OdinAttributeDrawer<DropdownIndexAttribute, int> 
    {
        private int currentIndex;
        private string[] names;
        private string error;
        private Dictionary<string, int> values = new Dictionary<string, int>();

        protected override void Initialize() 
        {
            ValueResolver<object> optionsValue = ValueResolver.Get<object>(Property, Attribute.Collection);
            error = optionsValue.ErrorMessage;

            if (!string.IsNullOrEmpty(error))
                return;

            IEnumerable collection = optionsValue.GetValue() as IEnumerable;

            // Error 
            if (collection == null) 
            {
                error = "Please, use a collection";
                return;
            }

            int count = collection.Cast<object>().Count();

            if (count == 0) 
            {
                error = "The collection is empty";
                return;
            }

            // Get field names
            names = GetFieldNames(collection);

            // Validate
            if (ValueEntry.SmartValue >= count) 
            {
                ValueEntry.SmartValue = 0;
            }

            // Fill Dictionary and set current Index
            for (int i = 0; i < names.Length; i++)
            {
                values.Add(names[i], i);
            }

            if (Attribute.OrderByName) 
            {
                string currentName = names[ValueEntry.SmartValue];
                names = names.OrderBy(n => n).ToArray();

                for (int i = 0; i < names.Length; i++) 
                {
                    if (names[i] == currentName) 
                    {
                        currentIndex = i;
                        break;
                    }
                }
            }
            else 
            {
                currentIndex = ValueEntry.SmartValue;
            }
        }

        private string[] GetFieldNames(IEnumerable collection)
        {
            string[] array = collection.AsQueryable().Select(Attribute.Expression).ToDynamicArray<string>().ToArray();

            if (Attribute.UseNiceString)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = array[i].GetNiceString();
                }
            }

            return array;

            object[] options = collection.Cast<object>().ToArray();
            if (options.Length > 0)
            {
                Func<object, string> func = options[0] switch
                {
                    Type => (obj) => (obj as Type).Name.GetNiceString(),
                    _ => (obj) => obj.ToString(),
                };

                return options.Select(func).ToArray();
            }

            return options.Select(obj => obj.ToString()).ToArray();
        }

        protected override void DrawPropertyLayout(GUIContent label) {

            if (!string.IsNullOrEmpty(error)) 
            {
                SirenixEditorGUI.ErrorMessageBox(error);
                CallNextDrawer(label);
                return;
            }

            EditorGUILayout.BeginHorizontal();

            int value = SirenixEditorFields.Dropdown(label, currentIndex, names);
            if (value != currentIndex) 
            {
                currentIndex = value;
                string name = names[value];
                ValueEntry.SmartValue = values[name];
            }

            if (Attribute.ShowRefreshButton) 
            {
                if (GUILayout.Button("↺", GUILayout.Width(30))) 
                {
                    values.Clear();
                    Initialize();
                }
            }
            
            EditorGUILayout.EndHorizontal();
        }
    }
}