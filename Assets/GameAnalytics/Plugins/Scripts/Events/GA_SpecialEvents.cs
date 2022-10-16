/// <summary>
/// This class handles special events unique to the Unity Wrapper, such as submitting level/scene changes, and delaying application quit
/// until data has been sent.
/// </summary>

using System.Collections;
using UnityEngine;

namespace GameAnalyticsSDK.Events
{
    public class GA_SpecialEvents : MonoBehaviour
    {
        /*[HideInInspector]
        public bool SubmitFpsAverage;
        [HideInInspector]
        public bool SubmitFpsCritical;
        [HideInInspector]
        public bool IncludeSceneChange;
        [HideInInspector]
        public int FpsCriticalThreshold;
        [HideInInspector]
        public int FpsSubmitInterval;*/

        #region private values

        private static int _frameCountAvg;
        private static float _lastUpdateAvg;
        private int _frameCountCrit;
        private float _lastUpdateCrit;

        private static int _criticalFpsCount;

        private static int _fpsWaitTimeMultiplier = 1;
        private static float _lastPauseStartTime;
        private static float _pauseDurationAvg;
        private static float _pauseDurationCrit;

        #endregion

        #region unity derived methods

        public void Start()
        {
            StartCoroutine(SubmitFPSRoutine());
            StartCoroutine(CheckCriticalFPSRoutine());
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (GameAnalytics.SettingsGA == null
                || (!GameAnalytics.SettingsGA.SubmitFpsAverage && !GameAnalytics.SettingsGA.SubmitFpsCritical))
                return;

            if (pauseStatus)
            {
                _lastPauseStartTime = Time.realtimeSinceStartup;
            }
            else
            {
                if (GameAnalytics.SettingsGA.SubmitFpsAverage)
                    _pauseDurationAvg += Time.realtimeSinceStartup - _lastPauseStartTime;

                if (GameAnalytics.SettingsGA.SubmitFpsCritical)
                    _pauseDurationCrit += Time.realtimeSinceStartup - _lastPauseStartTime;
            }
        }

        private IEnumerator SubmitFPSRoutine()
        {
            while (Application.isPlaying && GameAnalytics.SettingsGA != null &&
                   GameAnalytics.SettingsGA.SubmitFpsAverage)
            {
                var waitingTime = 30 * _fpsWaitTimeMultiplier;
                yield return new WaitForSecondsRealtime(waitingTime);
                _fpsWaitTimeMultiplier *= 2;
                SubmitFPS();
            }
        }

        private IEnumerator CheckCriticalFPSRoutine()
        {
            while (Application.isPlaying && GameAnalytics.SettingsGA != null &&
                   GameAnalytics.SettingsGA.SubmitFpsCritical)
            {
                yield return new WaitForSecondsRealtime(GameAnalytics.SettingsGA.FpsCirticalSubmitInterval);
                CheckCriticalFPS();
            }
        }

        public void Update()
        {
            //average FPS
            if (GameAnalytics.SettingsGA != null && GameAnalytics.SettingsGA.SubmitFpsAverage) _frameCountAvg++;

            //critical FPS
            if (GameAnalytics.SettingsGA != null && GameAnalytics.SettingsGA.SubmitFpsCritical) _frameCountCrit++;
        }

        public static void SubmitFPS()
        {
            //average FPS
            if (GameAnalytics.SettingsGA != null && GameAnalytics.SettingsGA.SubmitFpsAverage)
            {
                var timeSinceUpdate = Time.unscaledTime - _lastUpdateAvg - _pauseDurationAvg;
                _pauseDurationAvg = 0f;

                if (timeSinceUpdate > 1.0f)
                {
                    var fpsSinceUpdate = _frameCountAvg / timeSinceUpdate;
                    _lastUpdateAvg = Time.unscaledTime;
                    _frameCountAvg = 0;

                    if (fpsSinceUpdate > 0) GameAnalytics.NewDesignEvent("GA:AverageFPS", (int)fpsSinceUpdate);
                }
            }

            if (GameAnalytics.SettingsGA != null && GameAnalytics.SettingsGA.SubmitFpsCritical)
                if (_criticalFpsCount > 0)
                {
                    GameAnalytics.NewDesignEvent("GA:CriticalFPS", _criticalFpsCount);
                    _criticalFpsCount = 0;
                }
        }

        public void CheckCriticalFPS()
        {
            //critical FPS
            if (GameAnalytics.SettingsGA != null && GameAnalytics.SettingsGA.SubmitFpsCritical)
            {
                var timeSinceUpdate = Time.unscaledTime - _lastUpdateCrit - _pauseDurationCrit;
                _pauseDurationCrit = 0f;

                if (timeSinceUpdate >= 1.0f)
                {
                    var fpsSinceUpdate = _frameCountCrit / timeSinceUpdate;
                    _lastUpdateCrit = Time.unscaledTime;
                    _frameCountCrit = 0;

                    if (fpsSinceUpdate <= GameAnalytics.SettingsGA.FpsCriticalThreshold) _criticalFpsCount++;
                }
            }
        }

        #endregion
    }
}