using AdventureGame.Shared.ExtensionMethods;
using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas
{
    [Category(Categories.Components)]
    [Description("Set Renderer Material Color")]
    public class SetRendererMaterialColor : ActionTask<Renderer>
    {
        public BBParameter<string> property = "_Color";
        public BBParameter<Color> color;

        protected override string info => $"Set Color {agentInfo} = {GetColorInfo(color)}";

        protected override void OnExecute()
        {
            agent.material.SetColor(property.value, color.value);

            EndAction(true);
        }

        private string GetColorInfo(BBParameter<Color> parameter)
        {
            if (parameter.isDefined)
                return parameter.ToString();

            return parameter.value switch
            {
                Color color when color == Color.red => "Red".AddRichBold(),
                Color color when color == Color.green => "Green".AddRichBold(),
                Color color when color == Color.blue => "Blue".AddRichBold(),
                Color color when color == Color.clear => "Clear".AddRichBold(),
                Color color when color == Color.black => "Black".AddRichBold(),
                Color color when color == Color.white => "White".AddRichBold(),
                Color color when color == Color.gray  => "Gray".AddRichBold(),
                Color color when color == Color.yellow => "Yellow".AddRichBold(),
                Color color when color == Color.cyan => "Cyan".AddRichBold(),
                Color color when color == Color.magenta => "Magenta".AddRichBold(),
                _ => parameter.ToString(),
            };
        }
    }
}