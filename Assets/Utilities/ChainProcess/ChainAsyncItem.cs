using System;
using RH.Utilities.Processes.Base;

namespace RH.Utilities.Processes
{
    public class ChainAsyncItem : CustomProcess
    {
        private readonly Action<Action> _action;

        public ChainAsyncItem(Action<Action> action) : base(action.Method.Name)
        {
            _action = action;
        }

        public override void Execute()
        {
            SetState(ProcessState.Execute);
            _action.Invoke(Complete);
        }
    }
}