using System;
using RH.Utilities.Processes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RH.Utilities.SceneManagement.Core
{
    public static class SceneChanger
    {
        public static ChainMultiItem OnBeforeSceneChangedChain = new("[SceneChanger] OnBeforeSceneChangedChain");
        public static ChainMultiItem OnAfterSceneChangedChain = new("[SceneChanger] OnAfterSceneChangedChain");

        private static readonly ChainProcess _changeProcess = new("[SceneChanger] change process");

        private static Scene _currentScene;
        private static int _targetSceneId;

        static SceneChanger()
        {
            Init();
        }

        public static void ChangeScene(int buildIndex)
        {
            _targetSceneId = buildIndex;
            _changeProcess.Execute();
        }

        private static void Init()
        {
            _changeProcess.AddMultiItem(OnBeforeSceneChangedChain);
            _changeProcess.AddAsyncMethod(TryLoadNewScene);
            _changeProcess.AddAsyncMethod(HideCurrentScene);
            _changeProcess.AddAsyncMethod(ChangeScene);
            _changeProcess.AddMultiItem(OnAfterSceneChangedChain);

            _changeProcess.Completed += _changeProcess.Restart;
            Application.quitting += Dispose;
        }

        private static void Dispose()
        {
            _changeProcess.Completed -= _changeProcess.Restart;
            Application.quitting -= Dispose;
        }

        private static void HideCurrentScene(Action onDone)
        {
            if (_currentScene != default)
            {
                var reciever = _currentScene.GetSceneReciever();
                reciever.HideScene(onDone);
            }
            else
            {
                onDone.Invoke();
            }
        }

        private static void TryLoadNewScene(Action onDone)
        {
            var scene = SceneManager.GetSceneByBuildIndex(_targetSceneId);

            if (!scene.isLoaded)
                LoadScene(onDone);
            else
                onDone.Invoke();
        }

        private static void ChangeScene(Action onDone)
        {
            _currentScene = SceneManager.GetSceneByBuildIndex(_targetSceneId);
            var sceneReciever = _currentScene.GetSceneReciever();
            sceneReciever.ShowScene(onDone);
        }

        private static void LoadScene(Action onDone)
        {
            SceneManager.LoadSceneAsync(_targetSceneId, LoadSceneMode.Additive);
            SceneManager.sceneLoaded += ShowLoadedScene;

            void ShowLoadedScene(Scene scene, LoadSceneMode loadSceneMode)
            {
                SceneManager.sceneLoaded -= ShowLoadedScene;
                onDone.Invoke();
            }
        }
    }
}