using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using System;

namespace AdventureGame.Player.States
{
    [NodeDescription("Check the current state of the input")]
    public class CheckInputPS : PlayerCondition
    {
        public enum InputButton
        {
            Move,
            Jump,
            Dash,
            PrimarySkill,
            SecondarySkill
        }

        public Parameter<InputButton> inputButton;

        private Func<bool> inputValue;

        public override string Info => $"{name}: {inputButton}";

        public override void StartCondition()
        {
            inputValue = () => inputButton.Value switch
            {
                InputButton.Move => Inputs.MovePressed,
                InputButton.Jump => Inputs.JumpPressed,
                InputButton.Dash => Inputs.DashPressed,
                InputButton.PrimarySkill => Inputs.PrimarySkillPressed,
                InputButton.SecondarySkill => Inputs.SecondarySkillPressed,
                _ => throw new NotImplementedException(),
            };
        }

        public override bool CheckCondition() => inputValue();
    }
}