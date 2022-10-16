using UnityEngine;

public class InternetState : MonoBehaviour
{
    public bool isDebugInternet;

    public bool isHaveInternet;
    private bool isHaveInternetSavedState;

    private void Update()
    {
        isHaveInternet = Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork ||
                         Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;
        if (isHaveInternetSavedState != !isHaveInternet)
        {
            if (isHaveInternet)
                InternetAppeared();
            else
                InternetDropped();
        }

        isHaveInternetSavedState = !isHaveInternet;
    }

    private void OnApplicationPause(bool _isPause)
    {
        if (!_isPause)
            isHaveInternet = Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork ||
                             Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;
    }

    private void Initialize()
    {
    }

    public bool IsHaveNetTimeAndInternet()
    {
        return TimeManager.Instance.netTime != null && isHaveInternet;
    }

    public void InternetAppeared()
    {
        //InterstitialAdTimer.Instance.InterstitialAdShowed();
        TimeManager.Instance.ManualGetNetTime();
        CoroutineActions.WaitForConditionAndDoAction(() => TimeManager.Instance.netTime != null, () =>
        {
            //var time = new System.TimeSpan(0, GiftManager.Instance.giftData.GetCurrentGift().waitMinutes, 0);
            //TimeManager.Gift.Time_SetActiveNextGiftTime(time);
        });
    }

    public void InternetDropped()
    {
    }

    #region Singleton Init

    private static InternetState _instance;

    private void Awake() // Init in order
    {
        if (_instance == null)
            Init();
        else if (_instance != this)
            Destroy(gameObject);
    }

    public static InternetState Instance // Init not in order
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
        _instance = FindObjectOfType<InternetState>();
        _instance.Initialize();
    }

    #endregion
}