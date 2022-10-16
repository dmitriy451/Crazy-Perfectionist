using System;

namespace RH.Utilities.StateMachine
{
    public abstract class BaseState : IState
    {
        private readonly FinitStateMachine _stateMachine;

        public BaseState(FinitStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        protected bool isCurrentState => _stateMachine.CurrentState.Equals(this);
        protected bool stateMachineEnabled => _stateMachine.Enabled;

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void Update()
        {
        }

        protected void SwitchState(Type stateType)
        {
            _stateMachine.SwitchState(stateType);
        }
    }
}