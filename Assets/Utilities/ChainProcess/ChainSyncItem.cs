using System;
using RH.Utilities.Processes.Base;

namespace RH.Utilities.Processes
{
    public class ChainSyncItem : CustomProcess
    {
        private readonly Action _action;

        public ChainSyncItem(Action action) : base(action.Method.Name)
        {
            _action = action;
        }

        public override void Execute()
        {
            _action.Invoke();
            Complete();
        }
    }
}