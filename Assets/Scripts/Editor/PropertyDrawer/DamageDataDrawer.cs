using AdventureGame.BattleSystem;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;

namespace AdventureGame.Editor
{
    public class DamageDataDrawer : OdinAttributeProcessor<DamageData>
    {
        public override void ProcessSelfAttributes(InspectorProperty property, List<Attribute> attributes)
        {
            attributes.Add(new InlinePropertyAttribute());
            attributes.Add(new HideLabelAttribute());
            attributes.Add(new CustomBoxAttribute(true));
        }
    }
}