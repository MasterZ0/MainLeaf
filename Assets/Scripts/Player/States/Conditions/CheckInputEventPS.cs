using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using System;

namespace AdventureGame.Player.States
{
    [NodeDescription("It becomes true when the selected input changes to the desired state, disregarding the current state")]
    public class CheckInputEventPS : PlayerCondition
    {
        public enum InputEvent
        {
            JumpPressed,
            DashPressed,
            PrimarySkillPressed,
            SecondarySkillPressed
        }

        public Parameter<InputEvent> inputButton;

        private bool actionCalled;

        public override string Info => $"{name}: {inputButton}";

        public override void StartCondition() // Enable
        {
            actionCalled = false;
            switch (inputButton.Value)
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

        public override void StopCondition() // Disable
        {
            switch (inputButton.Value)
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

        public override bool CheckCondition()
        {
            bool value = actionCalled;
            actionCalled = false;
            return value;
        }
    }
}