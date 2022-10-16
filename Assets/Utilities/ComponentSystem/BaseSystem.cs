using System;
using UnityEngine;

namespace RH.Utilities.ComponentSystem
{
    public abstract class BaseSystem : IDisposable
    {
        protected BaseSystem()
        {
            Init();
        }

        public abstract void Dispose();

        protected abstract void Init();
    }

    public class EntryPoint : MonoBehaviour
    {
    }
}