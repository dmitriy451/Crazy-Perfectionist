using System;

namespace RH.Utilities.Processes.Base
{
    public abstract class CustomProcess : ICustomProcess
    {
        //For logging
        public readonly string Name;

        public CustomProcess(string name)
        {
            Name = name;
        }

        public ProcessState State { get; private set; }
        public bool IsComplete => State == ProcessState.Complete;

        public abstract void Execute();

        public virtual void Complete()
        {
            SetState(ProcessState.Complete);
        }

        public virtual void Restart()
        {
            SetState(ProcessState.Wait);
        }

        protected void SetState(ProcessState newState)
        {
            if (State == newState)
                throw new Exception($"Process {Name} can't switch to state {newState}, because it's a current state");
            State = newState;
        }
    }
}