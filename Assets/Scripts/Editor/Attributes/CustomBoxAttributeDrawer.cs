using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace AdventureGame.Editor
{
    public class CustomBoxAttributeDrawer : OdinAttributeDrawer<CustomBoxAttribute>
    {
        private LocalPersistentContext<bool> visible;

        protected override void Initialize()
        {
            base.Initialize();

            if (Attribute.Foldout)
            {
                visible = this.GetPersistentValue<bool>("Toggled");
            }
        }

        protected override void DrawPropertyLayout(GUIContent label)
        {
            if (Attribute.Foldout)
            {
                SirenixEditorGUI.BeginBox();
                {
                    SirenixEditorGUI.BeginBoxHeader();
                    {
                        visible.Value = SirenixEditorGUI.Foldout(visible.Value, Property.NiceName);
                    }
                    SirenixEditorGUI.EndBoxHeader();

                    if (SirenixEditorGUI.BeginFadeGroup(Property, visible.Value))
                    {
                        CallNextDrawer(label);
                    }
                    SirenixEditorGUI.EndFadeGroup();
                }
                SirenixEditorGUI.EndBox();
            }
            else
            {
                SirenixEditorGUI.BeginBox(Property.NiceName);
                {
                    CallNextDrawer(label);
                }
                SirenixEditorGUI.EndBox();
            }
        }
    }
}