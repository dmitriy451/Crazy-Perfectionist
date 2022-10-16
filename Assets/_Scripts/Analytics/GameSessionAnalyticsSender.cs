using System;
using UnityEngine;
//using GameAnalyticsSDK;


public class GameSessionAnalyticsSender : MonoBehaviour
{
    private DateTime _startTime;

    //private void Awake() => GameAnalytics.Initialize();

    private void Start()
    {
        SetStartTime();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            SendQuitEvent();
        else
            SetStartTime();
    }

    private void OnApplicationQuit()
    {
        SendQuitEvent();
    }

    private void SetStartTime()
    {
        _startTime = DateTime.Now;
    }

    private void SendQuitEvent()
    {
    } /*=> GameAnalytics.NewDesignEvent("GamePause", (float)DateTime.Now.Subtract(_startTime).TotalSeconds);*/
}