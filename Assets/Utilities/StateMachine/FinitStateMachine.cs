using System;
using System.Collections.Generic;

namespace RH.Utilities.StateMachine
{
    public class FinitStateMachine
    {
        private List<IState> _states;
        public bool Enabled { get; } = true;
        public IState CurrentState { get; private set; }

        public void AddStates(IState firstState, params IState[] states)
        {
            CurrentState = firstState;

            if (_states == null)
                _states = new List<IState>();

            _states.Add(firstState);
            _states.AddRange(states);

            EnterState(firstState);
        }

        public void SwitchState(Type stateType)
        {
            CheckSwitchPossibility(stateType, out var newState);
            EnterState(newState);
        }

        public void Update()
        {
            if (Enabled)
                CurrentState.Update();
            else
                throw new Exception("Can't update disabled state machine");
        }

        private void EnterState(IState state)
        {
            CurrentState?.Exit();
            CurrentState = state;
            state.Enter();
        }

        private void CheckSwitchPossibility(Type stateType, out IState newState)
        {
            if (CurrentState.GetType().Equals(stateType))
                throw new Exception(
                    $"[StateMachine] - Can't switch state from {stateType.Name} to {CurrentState.GetType().Name}");

            newState = _states.Find(state => state.GetType().Equals(stateType));

            if (newState == null)
                throw new Exception($"[StateMachine] - Doesn't contains state {stateType.Name}");
        }
    }
}