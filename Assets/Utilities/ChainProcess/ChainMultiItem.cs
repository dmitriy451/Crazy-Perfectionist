using System;
using System.Collections.Generic;
using RH.Utilities.Processes.Base;

namespace RH.Utilities.Processes
{
    public class ChainMultiItem : CustomProcess
    {
        private readonly List<Action<Action>> _asyncActions;
        private readonly List<Action> _syncActions;

        private int _callbacksCount;

        public ChainMultiItem(string name) : base(name)
        {
            _asyncActions = new List<Action<Action>>();
            _syncActions = new List<Action>();

            _callbacksCount = 0;
        }

        public ChainMultiItem(string name, Action<Action>[] async, Action[] sync) : this(name)
        {
            AddAsyncMethods(async);
            AddSyncMethods(sync);
        }

        public void AddAsyncMethods(params Action<Action>[] actions)
        {
            _asyncActions.AddRange(actions);
        }

        public void AddSyncMethods(params Action[] actions)
        {
            _syncActions.AddRange(actions);
        }

        public void AddAsyncMethod(Action<Action> action)
        {
            _asyncActions.Add(action);
        }

        public void AddSyncMethod(Action action)
        {
            _syncActions.Add(action);
        }

        public override void Restart()
        {
            _callbacksCount = 0;
            base.Restart();
        }

        public override void Execute()
        {
            //NOTE: Порядок методов важен! Если будет наоборот, то при отсутствии асинхронных методов, синхронные вообще не вызовутся.
            ExecuteSyncMethods();
            ExecuteAsyncMethods();
        }

        private void ExecuteSyncMethods()
        {
            if (_syncActions == null)
                return;

            foreach (var action in _syncActions)
                action.Invoke();
        }

        private void ExecuteAsyncMethods()
        {
            if (_asyncActions == null || _asyncActions.Count == 0)
            {
                Complete();
                return;
            }

            foreach (var action in _asyncActions)
                action.Invoke(TryComplete);
        }

        private void TryComplete()
        {
            _callbacksCount++;

            if (_callbacksCount == _asyncActions.Count)
                Complete();
        }
    }
}