namespace RH.Utilities.Processes.Base
{
    public interface ICustomProcess
    {
        bool IsComplete { get; }

        void Execute();
        void Complete();
        void Restart();
    }
}