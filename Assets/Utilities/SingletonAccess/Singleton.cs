using System;

namespace RH.Utilities.SingletonAccess
{
    public abstract class Singleton<TSelf> where TSelf : Singleton<TSelf>
    {
        protected Singleton()
        {
            if (Instance != null)
                throw new Exception($"[Singleton] - {typeof(TSelf).Name} already instantiated.");

            Instance = this as TSelf;
        }

        public static TSelf Instance { get; private set; }

        public static void DestroyInstance()
        {
            if (Instance == null)
                throw new Exception($"[Singleton] - {typeof(TSelf).Name} try to destroy without Instance.");

            Instance.PrepareToDestroy();
            Instance = null;
        }

        protected virtual void PrepareToDestroy()
        {
        }
    }
}