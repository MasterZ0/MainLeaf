using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System;
using Sirenix.Utilities;
using System.Reflection;
using AdventureGame.Shared;
using AdventureGame.Shared.ExtensionMethods;

namespace AdventureGame.Editor {
    public class TypeSelectionAttributeDrawer<TList, TElement> : OdinAttributeDrawer<TypeSelectionAttribute, TList> where TList : IEnumerable<TElement> {

        private string error;
        private string typeName;
        private string[] typeValues;

        private Dictionary<string, Type> allTypes;
        private Type elementType;

        private Func<IEnumerable<TElement>> getSelection;
        private Func<IEnumerable<string>> usedValues;
        private LocalPersistentContext<bool> isToggled;

        //private GUITableRowLayoutGroup table = new GUITableRowLayoutGroup(); TODO: Create list borders; //table.BeginCell(index);
        protected override void Initialize() {
            elementType = (Property.ChildResolver as IOrderedCollectionResolver).ElementType;

            // Get all types
            allTypes = new Dictionary<string, Type>();
            List<Type> types = new List<Type>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                if (assembly.FullName.StartsWith("AdventureGame"))
                {
                    types.AddRange(assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && (t.IsSubclassOf(elementType) || t == elementType)));
                }
            }

            typeValues = new string[types.Count];
            for (int i = 0; i < types.Count; i++)
            {
                string niceName = types[i].Name.GetNiceString();
                typeValues[i] = niceName;
                allTypes.Add(niceName, types[i]);
            }

            if (types.Count == 0)
            {
                error = "Vai se fuder";
                return;
            }

            isToggled = this.GetPersistentValue<bool>("Toggled");
            typeName = elementType.Name.GetNiceString();


            getSelection = () => Property.Children.Select(x => (TElement)x.ValueEntry.WeakSmartValue);
            usedValues = () => getSelection().Select(x => x.GetType().Name.GetNiceString());

            Validate(getSelection().ToArray());
        }

        // Repeted validation 
        private void Validate(TElement[] currentSelection)
        {
            Dictionary<string, TElement> selection = new Dictionary<string, TElement>();
            bool reload = false;
            foreach (var value in currentSelection)
            {
                string key = value.GetType().Name;
                if (string.IsNullOrEmpty(key) || selection.ContainsKey(key))
                {
                    reload = true;
                }
                else
                {
                    selection.Add(key, value);
                }
            }

            if (reload)
            {
                SetValue(selection.Values);
            }
        }

        protected override void DrawPropertyLayout(GUIContent label) {

            // Draw error
            if (!string.IsNullOrEmpty(error))
            {
                SirenixEditorGUI.ErrorMessageBox(error);
                CallNextDrawer(label);
                return;
            }

            // Foldout
            SirenixEditorGUI.BeginHorizontalToolbar();
            isToggled.Value = SirenixEditorGUI.Foldout(isToggled.Value, label);
            int children = Property.Children.Count;

            GUILayout.FlexibleSpace();

            // Add button and children count
            EditorGUILayout.LabelField(children == 0 ? "Empty" : $"{children} Items", SirenixGUIStyles.RightAlignedGreyMiniLabel);
            if (SirenixEditorGUI.ToolbarButton(EditorIcons.Plus))
            {
                Rect windowRect = new Rect(Event.current.mousePosition, Vector2.zero);
                ShowSelector(windowRect);
            }
            SirenixEditorGUI.EndHorizontalToolbar();

            // Draw collection properties
            if (SirenixEditorGUI.BeginFadeGroup(this, isToggled.Value))
            {

                SirenixEditorGUI.BeginVerticalList();

                ++EditorGUI.indentLevel;

                for (int index = 0; index < children; index++)
                {
                    SirenixEditorGUI.BeginListItem(false);
                    DrawElement(Property.Children[index], index);
                    SirenixEditorGUI.EndListItem();
                }
                --EditorGUI.indentLevel;
                SirenixEditorGUI.EndVerticalList();
            }
            SirenixEditorGUI.EndFadeGroup();
        }

        private void DrawElement(InspectorProperty property, int index) {
            EditorGUILayout.BeginHorizontal();

            // Title and Property
            EditorGUILayout.BeginVertical();
            string niceName = ObjectNames.NicifyVariableName(((TElement)property.ValueEntry.WeakSmartValue).GetType().Name);
            SirenixEditorGUI.Title(niceName, string.Empty, Attribute.TextAlignment, Attribute.HorizontalLine, Attribute.BoldLabel);
            property.Draw(null);
            GUILayout.Space(5); 
            EditorGUILayout.EndVertical();

            // Remove btn
            EditorGUILayout.BeginVertical(GUILayout.Width(20), GUILayout.MaxHeight(500));   // ExpandedHeight is not working
            GUILayout.FlexibleSpace();
            if (SirenixEditorGUI.IconButton(GUILayoutUtility.GetRect(20f, 20f).AlignCenter(15f), EditorIcons.X))
            {
                (Property.ChildResolver as IOrderedCollectionResolver).QueueRemoveAt(index);
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();
        }

        private OdinSelector<object> ShowSelector(Rect rect)
        {
            GenericSelector<object> genericSelector = new GenericSelector<object>(typeName, false, typeValues);
            genericSelector.CheckboxToggle = true;
            genericSelector.DrawConfirmSelectionButton = true;
            genericSelector.SelectionTree.Config.DrawSearchToolbar = true;
            genericSelector.FlattenedTree = true;

            genericSelector.SetSelection(usedValues().ToArray());
            genericSelector.SelectionTree.EnumerateTree().AddThumbnailIcons(true);

            genericSelector.SelectionConfirmed += OnSelectionConfirmed;
            genericSelector.ShowInPopup(rect);
            return genericSelector;
        }

        private void OnSelectionConfirmed(IEnumerable<object> result)
        {
            string[] resultCollection = result.Select(s => s.ToString()).ToArray();

            // Filtered list is not inside list
            List<TElement> validSelection = getSelection().Where(s =>
            {
                // Already have a value?
                string selection = s.GetType().Name;
                return resultCollection.FirstOrDefault(c => c == selection) != null;
            }).ToList();

            List<string> currentParameters = validSelection.Select(x => x.GetType().Name).ToList();

            // Create new instances
            for (int i = 0; i < resultCollection.Length; i++)
            {

                if (!currentParameters.Contains(resultCollection[i]))
                {
                    Type type = allTypes[resultCollection[i]];
                    TElement newElement = (TElement)Activator.CreateInstance(type);
                    validSelection.Add(newElement);
                }
                    
            }

            // Set collection
            SetValue(validSelection);
        }

        private void SetValue(IEnumerable<TElement> elements)
        {
            // Reorder
            elements = elements.OrderBy(x => x.GetType().Name);

            if (ValueEntry.WeakSmartValue.GetType().IsArray)
            {
                ValueEntry.WeakSmartValue = elements.ToArray();
            }
            else if (ValueEntry.WeakSmartValue is IList)
            {
                ValueEntry.WeakSmartValue = elements.ToList();
            }
        }
    }
}
