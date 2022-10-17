using AdventureGame.BattleSystem;
using AdventureGame.Data;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;

namespace AdventureGame.Editor
{
    public abstract class CustomBoxProcessor<T> : OdinAttributeProcessor<T>
    {
        public override void ProcessSelfAttributes(InspectorProperty property, List<Attribute> attributes)
        {
            attributes.Add(new InlinePropertyAttribute());
            attributes.Add(new HideLabelAttribute());
            attributes.Add(new CustomBoxAttribute(true));
        }
    }

    public class DamageDataProcessor : CustomBoxProcessor<DamageData> { }
    public class AIPathParametersProcessor : CustomBoxProcessor<AIPathParameters> { }
}