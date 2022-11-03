using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System;

namespace AdventureGame.Player.States
{
    [Description("Check the current state of the input")]
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

        public BBParameter<InputButton> inputButton;

        private Func<bool> inputValue;

        protected override string info => $"{name}: {inputButton}";

        protected override void OnEnable()
        {
            inputValue = () => inputButton.value switch
            {
                InputButton.Move => Inputs.MovePressed,
                InputButton.Jump => Inputs.JumpPressed,
                InputButton.Dash => Inputs.DashPressed,
                InputButton.PrimarySkill => Inputs.PrimarySkillPressed,
                InputButton.SecondarySkill => Inputs.SecondarySkillPressed,
                _ => throw new NotImplementedException(),
            };
        }

        protected override bool OnCheck() => inputValue();
    }
}