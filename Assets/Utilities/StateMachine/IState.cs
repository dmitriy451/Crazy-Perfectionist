﻿namespace RH.Utilities.StateMachine
{
    public interface IState
    {
        void Enter();
        void Update();
        void Exit();
    }
}