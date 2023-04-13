/*using Z3.UIBuilder.Core.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Sirenix.Utilities;

namespace AdventureGame.Editor
{
    public class Vector2SliderAttributeDrawer : OdinAttributeDrawer<VectorSlider, Vector2>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {

            Rect rect = EditorGUILayout.GetControlRect();

            if (label != null)
                rect = EditorGUI.PrefixLabel(rect, label);

            Vector2 value = ValueEntry.SmartValue;

            GUIHelper.PushLabelWidth(20);

            value.x = EditorGUI.Slider(rect.AlignLeft(rect.width * .5f), "X", value.x, Attribute.Range.x, Attribute.Range.y);
            value.y = EditorGUI.Slider(rect.AlignRight(rect.width * .5f), "Y", value.y, Attribute.Range.x, Attribute.Range.y);
            
            GUIHelper.PopLabelWidth();
           
            ValueEntry.SmartValue = value; 
        }
    }
    public class Vector3SliderAttributeDrawer : OdinAttributeDrawer<VectorSlider, Vector3>
    {
        private const float oneThird = 1f / 3f;
        protected override void DrawPropertyLayout(GUIContent label)
        {

            Rect rect = EditorGUILayout.GetControlRect();

            if (label != null)
                rect = EditorGUI.PrefixLabel(rect, label);

            Vector3 value = ValueEntry.SmartValue;

            GUIHelper.PushLabelWidth(20);

            value.x = EditorGUI.Slider(rect.AlignLeft(rect.width * oneThird), "X", value.x, Attribute.Range.x, Attribute.Range.y);
            value.y = EditorGUI.Slider(rect.AlignCenter(rect.width * oneThird), "Y", value.y, Attribute.Range.x, Attribute.Range.y);
            value.z = EditorGUI.Slider(rect.AlignRight(rect.width * oneThird), "Z", value.z, Attribute.Range.x, Attribute.Range.y);

            GUIHelper.PopLabelWidth();

            ValueEntry.SmartValue = value;
        }
    }
}*/