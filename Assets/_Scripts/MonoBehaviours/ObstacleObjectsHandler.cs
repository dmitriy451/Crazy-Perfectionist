using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NotPerfectObject))]
public class ObstacleObjectsHandler : MonoBehaviour
{
    [SerializeField] private Transform obstaclesParent;
    private readonly List<ObstacleObject> obstacleObjects = new();
    private readonly List<Transform> obstaclesParentChildren = new();

    private void Awake()
    {
        obstaclesParentChildren.AddRange(obstaclesParent.GetComponentsInChildren<Transform>());
        obstaclesParentChildren.Remove(obstaclesParent);
        foreach (var _obstaclesParentChild in obstaclesParentChildren)
            if (!_obstaclesParentChild.gameObject.GetComponent<ObstacleObject>())
                _obstaclesParentChild.gameObject.AddComponent<ObstacleObject>();
        obstacleObjects.AddRange(GetComponentsInChildren<ObstacleObject>());

        var _object = GetComponent<NotPerfectObject>();
        _object.OnInitObject.AddListener(x => Disable());
        //_object.OnLastBehTapObject.AddListener(Enable);
        //_object.OnObjectFail.AddListener(Failed);
        //_object.OnCompleteObject.AddListener((x) => { if (x <= DataManager.Instance.balanceData.ThresholdForPassingLevel) Failed(); });
    }

    private void Failed()
    {
        foreach (var _obstacleObject in obstacleObjects)
            _obstacleObject.FailRigidbodyActivate();
    }

    public void Enable()
    {
        foreach (var _obstacleObject in obstacleObjects)
            _obstacleObject.Enable();
    }

    public void Disable()
    {
        foreach (var _obstacleObject in obstacleObjects)
            _obstacleObject.Disable();
    }
}