using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System;

namespace AdventureGame.Player.States
{
    [Description("It becomes true when the selected input changes to the desired state, disregarding the current state")]
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

        protected override bool OnCheck()
        {
            bool value = actionCalled;
            actionCalled = false;
            return value;
        }
    }
}