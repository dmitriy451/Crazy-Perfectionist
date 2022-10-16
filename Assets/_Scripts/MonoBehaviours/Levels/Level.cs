using System.Collections.Generic;
using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform generalViewCameraPoint;
    [SerializeField] private ParticleSystem levelCompletePS;
    [SerializeField] private ParticleSystem levelFailPS;
    [HideInInspector] public UnityEvent OnLevelComplete;
    private NotPerfectObject currentNotPerfectObject;
    private bool isObjectSelected;
    private int notPerfectObjectCounter;

    private readonly List<NotPerfectObject> notPerfectObjects = new();

    private void Start()
    {
        DataManager.Instance.mainData.LevelAccuracy = 0;
        GlobalEvents.OnTapAtObject.AddListener(GoToTapObject);
        UIEvents.UpdateLevelProgressBar?.Invoke(0.0f);
        UIEvents.ChangeLevelText?.Invoke($"LEVEL {DataManager.Instance.mainData.LevelNumber + 1}");
        notPerfectObjects.AddRange(GetComponentsInChildren<NotPerfectObject>());
        notPerfectObjectCounter = 0;
        ToGeneralView();
        if (!DataManager.Instance.mainData.TutorialTapToFixCompleted)
            UIEvents.TutorialTapToFixTextShow.Invoke(true);
        //GoToNextObject();
    }

    private void GoToNextObject()
    {
        ToGeneralView();
        CoroutineActions.ExecuteAction(0.2f, () =>
        {
            CameraController.Instance.SetAndMoveToTarget(notPerfectObjects[notPerfectObjectCounter].GetCameraPoint());
            notPerfectObjects[notPerfectObjectCounter].OnCompleteObject.AddListener(CurrentObjectComplete);
            notPerfectObjects[notPerfectObjectCounter].StartObject();
        });
    }

    private void GoToTapObject(NotPerfectObject _notPerfectObject)
    {
        if (isObjectSelected)
            return;

        if (notPerfectObjectCounter > notPerfectObjects.Count)
            return;

        isObjectSelected = true;

        if (!DataManager.Instance.mainData.TutorialTapToFixCompleted)
        {
            DataManager.Instance.mainData.TutorialTapToFixCompleted = true;
            UIEvents.TutorialTapToFixTextShow.Invoke(false);
        }

        CoroutineActions.ExecuteAction(0.3f, () =>
        {
            CameraController.Instance.SetAndMoveToTarget(_notPerfectObject.GetCameraPoint());
            _notPerfectObject.OnCompleteObject.RemoveAllListeners();
            _notPerfectObject.OnObjectFail.RemoveAllListeners();
            _notPerfectObject.OnCompleteObject.AddListener(CurrentObjectComplete);
            _notPerfectObject.OnObjectFail.AddListener(CurrentObjectFailed);
            _notPerfectObject.StartObject();
            currentNotPerfectObject = _notPerfectObject;
            if (!DataManager.Instance.mainData.TutorialTapOnTimeCompleted)
                UIEvents.TutorialTapOnTimeTextShow.Invoke(true);
        });
    }

    private void CurrentObjectComplete(int _accuracy)
    {
        ToGeneralView();
        isObjectSelected = false;
        if (_accuracy < DataManager.Instance.balanceData.ThresholdForPassingLevel)
        {
            CurrentObjectFailed();
            return;
        }

        if (_accuracy >= DataManager.Instance.balanceData.ThresholdForPassingLevel)
        {
            notPerfectObjectCounter++;
            if (!DataManager.Instance.mainData.TutorialTapOnTimeCompleted)
            {
                DataManager.Instance.mainData.TutorialTapOnTimeCompleted = true;
                UIEvents.TutorialTapOnTimeTextShow.Invoke(false);
            }
        }

        GlobalEvents.AddLevelAccuracy.Invoke(_accuracy);
        UIEvents.UpdateLevelProgressBar?.Invoke(notPerfectObjectCounter / (float)notPerfectObjects.Count);
        GlobalEvents.OnObjectComplete?.Invoke(_accuracy);
        
        if (notPerfectObjectCounter >= notPerfectObjects.Count)
            CompleteLevel();
        //else
        //    GoToNextObject();
    }

    private void CurrentObjectFailed()
    {
        ToGeneralView();
        currentNotPerfectObject.OnObjectCanRetry.Invoke();
        //CoroutineActions.ExecuteAction(0.5f, () => { GlobalEvents.OnLevelFailBoom?.Invoke(); levelFailPS.Play(); MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.Failure); });
        //CoroutineActions.ExecuteAction(1.0f, () => { GlobalEvents.OnLevelFailed?.Invoke(); });
    }

    private void ToGeneralView()
    {
        CameraController.Instance.SetAndMoveToTarget(generalViewCameraPoint);
    }

    private void CompleteLevel()
    {
        ToGeneralView();
        DataManager.Instance.mainData.LevelAccuracy /= notPerfectObjects.Count;
        if (DataManager.Instance.mainData.LevelAccuracy > DataManager.Instance.balanceData.ThresholdForPassingLevel)
        {
            CoroutineActions.ExecuteAction(0.75f, () =>
            {
                OnLevelComplete.Invoke();
                levelCompletePS.Play();
                MMVibrationManager.Haptic(HapticTypes.Success);
            });
            CoroutineActions.ExecuteAction(1.5f,
                () => { GlobalEvents.OnLevelComplete?.Invoke(DataManager.Instance.mainData.LevelNumber); });
        }
        else
        {
            CoroutineActions.ExecuteAction(0.75f, () =>
            {
                GlobalEvents.OnLevelFailBoom?.Invoke();
                levelFailPS.Play();
                MMVibrationManager.Haptic(HapticTypes.Failure);
            });
            CoroutineActions.ExecuteAction(1.5f, () => { GlobalEvents.OnLevelFailed?.Invoke(); });
        }
    }
}