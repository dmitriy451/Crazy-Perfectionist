using System;
using RH.Utilities.SceneManagement.Core;
using UnityEngine;

namespace RH.Utilities.SceneManagement
{
    public abstract class BaseSceneReciever : MonoBehaviour, ISceneReciever
    {
        private bool _isInitialized;

        public void ShowScene(Action onDone)
        {
            if (!_isInitialized)
            {
                Init();
                _isInitialized = true;
            }

            AttachListeners();
            ShowContent(onDone);
        }

        public void HideScene(Action onDone)
        {
            DetachListener();
            HideContent(onDone);
        }

        protected virtual void Init()
        {
        }

        protected virtual void AttachListeners()
        {
        }

        protected abstract void ShowContent(Action onDone);

        protected virtual void DetachListener()
        {
        }

        protected abstract void HideContent(Action onDone);
    }
}