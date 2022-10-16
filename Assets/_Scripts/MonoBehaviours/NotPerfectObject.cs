using System.Collections;
using System.Collections.Generic;
using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.Events;

public class NotPerfectObject : MonoBehaviour
{
    [SerializeField] public Transform objectParent;
    [SerializeField] private Transform cameraPoint;

    [HideInInspector] public GameObject thisObject;
    [HideInInspector] public UnityEvent<GameObject> OnInitObject;
    [HideInInspector] public UnityEvent OnRandomizeObject;
    [HideInInspector] public UnityEvent OnPickUpObject;
    [HideInInspector] public UnityEvent OnStartObject;
    [HideInInspector] public UnityEvent OnLastBehTapObject;
    [HideInInspector] public UnityEvent OnObjectFail;
    [HideInInspector] public UnityEvent OnObjectCanRetry;
    [HideInInspector] public UnityEvent OnReadyToCheckObject;
    [HideInInspector] public UnityEvent<int> OnPreCompleteObject;
    [HideInInspector] public UnityEvent<int> OnCompleteObject; // accuracy
    private bool isFailed;
    private bool isSelected;

    private NotPerfectObjectBehaviour[] objectBehaviours;
    private int objectBehavioursCounter;
    private Rigidbody rigidbody;

    private void Awake()
    {
        thisObject = objectParent.GetChild(0).gameObject;
        rigidbody = thisObject.GetComponentInChildren<Rigidbody>();
    }

    private void Start()
    {
        OnInitObject.Invoke(thisObject);
        OnRandomizeObject.Invoke();
        BehaviourRegistration();
        UIEvents.ObjectButtonTap.AddListener(Tap);
        //OnObjectFail.AddListener(Fail);
    }

    private void Tap()
    {
        if (isSelected)
        {
            MMVibrationManager.Haptic(HapticTypes.LightImpact);
            objectBehaviours[objectBehavioursCounter].Tap();
            objectBehavioursCounter++;

            if (objectBehavioursCounter >= objectBehaviours.Length)
            {
                OnLastBehTapObject.Invoke();
                StartCoroutine(ReadyToCheck());
            }

            if (objectBehavioursCounter < objectBehaviours.Length)
                objectBehaviours[objectBehavioursCounter].RunBehaviour();
        }
    }

    public Transform GetCameraPoint()
    {
        return cameraPoint;
    }

    private void BehaviourRegistration()
    {
        var _behs = new List<NotPerfectObjectBehaviour>();
        _behs.AddRange(GetComponents<NotPerfectObjectBehaviour>());
        objectBehaviours = new NotPerfectObjectBehaviour[_behs.Count];

        for (var i = 0; i < objectBehaviours.Length; i++)
            foreach (var _beh in _behs)
                if (_beh.runQueueNum == i)
                    objectBehaviours[i] = _beh;
    }

    public void StartObject()
    {
        objectBehavioursCounter = 0;
        isSelected = true;
        OnPickUpObject.Invoke();
        CoroutineActions.ExecuteAction(DataManager.Instance.balanceData.PickUpTime, () =>
        {
            OnStartObject.Invoke();
            objectBehaviours[objectBehavioursCounter].RunBehaviour();
        });
    }

    public void MatchCheckComplete(int _accuracy)
    {
        CoroutineActions.ExecuteAction(DataManager.Instance.balanceData.PickUpTime,
            () => { MatchCheckInter(_accuracy); });
    }

    private IEnumerator ReadyToCheck()
    {
        yield return new WaitForSeconds(0.0f);

        isSelected = false;
        OnReadyToCheckObject.Invoke();
    }

    private void Fail()
    {
        isFailed = true;
        isSelected = false;
        StopCoroutine(ReadyToCheck());
    }

    private void MatchCheckInter(int _accuracy)
    {
        if (!isFailed)
        {
            OnPreCompleteObject.Invoke(_accuracy);
            MMVibrationManager.Haptic(HapticTypes.MediumImpact);
            OnCompleteObject.Invoke(_accuracy);
        }
    }
}