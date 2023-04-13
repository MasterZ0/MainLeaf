using AdventureGame.Shared.ExtensionMethods;
using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;

namespace AdventureGame.NodeCanvas
{
    [NodeCategory(Categories.Components)]
    [NodeDescription("Set Renderer Material Color")]
    public class SetRendererMaterialColor : ActionTask<Renderer>
    {
        public Parameter<string> property = "_Color"; // You can see properties in inspector by debug like "_UnlitColor"
        public Parameter<Color> color;

        public override string Info => $"Set Color {AgentInfo} = {GetColorInfo(color)}";

        protected override void StartAction()
        {
            Agent.material.SetColor(property.Value, color.Value);

            EndAction(true);
        }

        private string GetColorInfo(Parameter<Color> parameter)
        {
            if (parameter.isDefined)
                return parameter.ToString();

            return parameter.Value switch
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