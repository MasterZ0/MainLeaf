using NodeCanvas.Framework;
using System;

namespace AdventureGame.Player.States
{
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

    public class CheckInputEventPS : PlayerCondition
    {
        public enum InputEvent
        {
            JumpPressed,
            DashPressed,
            PrimarySkillPressed,
            SecondarySkillPressed
        }

        public BBParameter<InputEvent> inputButton;

        private bool actionCalled;

        protected override string info => $"{name}: {inputButton}";

        protected override void OnEnable()
        {
            actionCalled = false;
            switch (inputButton.value)
            {
                case InputEvent.JumpPressed:
                    Inputs.OnJumpPressed += OnCallAction;
                    break;
                case InputEvent.DashPressed:
                    Inputs.OnDashPressed += OnCallAction;
                    break;
                case InputEvent.PrimarySkillPressed:
                    Inputs.OnPrimarySkillPressed += OnCallAction;
                    break;
                case InputEvent.SecondarySkillPressed:
                    Inputs.OnSecondarySkillPressed += OnCallAction;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        protected override void OnDisable()
        {
            switch (inputButton.value)
            {
                case InputEvent.JumpPressed:
                    Inputs.OnJumpPressed -= OnCallAction;
                    break;
                case InputEvent.DashPressed:
                    Inputs.OnDashPressed -= OnCallAction;
                    break;
                case InputEvent.PrimarySkillPressed:
                    Inputs.OnPrimarySkillPressed -= OnCallAction;
                    break;
                case InputEvent.SecondarySkillPressed:
                    Inputs.OnSecondarySkillPressed -= OnCallAction;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void OnCallAction() => actionCalled = true;

        protected override bool OnCheck() => actionCalled;
    }
}