using UnityEngine.Events;

public static class GlobalEvents
{
    //Money
    public static UnityEvent<int> AddMoney = new();
    public static UnityEvent<int> MoneyAdded = new();

    //LevelAccuracy
    public static UnityEvent<int> AddLevelAccuracy = new();
    public static UnityEvent<int> LevelAccuracyAdded = new();

    //Camera
    public static UnityEvent MoveCameraToExamplePos = new();

    //Settings
    public static UnityEvent<bool> SetVibrationState = new();

    public static UnityEvent<int> OnLevelStart = new();
    public static UnityEvent<int> OnLevelComplete = new();
    public static UnityEvent OnLevelFailed = new();
    public static UnityEvent OnLevelFailBoom = new();

    //Object
    public static UnityEvent<int> OnObjectComplete = new();
    public static UnityEvent<NotPerfectObject> OnTapAtObject = new();
}