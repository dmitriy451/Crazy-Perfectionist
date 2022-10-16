//using AppsFlyerSDK;
//using Firebase.Analytics;

using System.Collections.Generic;
using GameAnalyticsSDK;
using UnityEngine;

public class AnalyticManager : MonoBehaviour
{
    private void OnApplicationPause(bool pause)
    {
        if (pause)
            LogEventWithAllData("game_quit");
    }

    private void OnApplicationQuit()
    {
        LogEventWithAllData("game_quit");
    }

    private void Initialize()
    {
        GameAnalytics.Initialize();
    }

    public void LogEvent(string _event)
    {
        MyFacebook.Instance.LogEvent(_event);
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, _event);
        //FirebaseAnalytics.LogEvent(_event, "level", Stats.getInstanse.lvl);
        //AppsFlyer.sendEvent(_event, _params);
        //MyTenjin.Instance.instance.SendEvent(_event);
    }

    public void LogEventWithAllData(string _event)
    {
        var _params = new Dictionary<string, object>();
        _params.Add("level_number", DataManager.Instance.mainData.LevelNumber);
        _params.Add("money", DataManager.Instance.mainData.Money);
        //_params.Add("time", GameDatabase.Instance.gameData.CurrentPicture.DonePaintTimeInSeconds);


        MyFacebook.Instance.LogEvent(_event, _params);
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, _event, _params);
        //FirebaseAnalytics.LogEvent(_event, "level", Stats.getInstanse.lvl);
        //AppsFlyer.sendEvent(_event, _params);
        //MyTenjin.Instance.instance.SendEvent(_event);
    }

    public void LogEvent_OnLevelStart()
    {
        var _params = new Dictionary<string, object>();
        _params.Add("level_number", DataManager.Instance.mainData.LevelNumber);

        MyFacebook.Instance.LogEvent("level_start", _params);
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "level_start",
            DataManager.Instance.mainData.LevelNumber);
        //AppMetrica.Instance.ReportEvent("level_start", _params);
        //AppMetrica.Instance.SendEventsBuffer();
    }

    public void LogEvent_OnLevelFinish()
    {
        var _params = new Dictionary<string, object>();
        _params.Add("level_number", DataManager.Instance.mainData.LevelNumber);

        MyFacebook.Instance.LogEvent("level_finish", _params);
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "level_finish",
            DataManager.Instance.mainData.LevelNumber);
        //AppMetrica.Instance.ReportEvent("level_finish", _params);
        //AppMetrica.Instance.SendEventsBuffer();
    }

    public void LogEvent_OnLevelFailed()
    {
        var _params = new Dictionary<string, object>();
        _params.Add("level_number", DataManager.Instance.mainData.LevelNumber);

        MyFacebook.Instance.LogEvent("level_failed", _params);
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "level_failed",
            DataManager.Instance.mainData.LevelNumber);
        //AppMetrica.Instance.ReportEvent("level_finish", _params);
        //AppMetrica.Instance.SendEventsBuffer();
    }

    public void LogEvent(string _event, Dictionary<string, object> _params)
    {
    }

    #region Singleton Init

    private static AnalyticManager _instance;

    private void Awake() // Init in order
    {
        if (_instance == null)
            Init();
        else if (_instance != this)
            Destroy(gameObject);
    }

    public static AnalyticManager Instance // Init not in order
    {
        get
        {
            if (_instance == null)
                Init();
            return _instance;
        }
        private set => _instance = value;
    }

    private static void Init() // Init script
    {
        _instance = FindObjectOfType<AnalyticManager>();
        _instance.Initialize();
    }

    #endregion
}