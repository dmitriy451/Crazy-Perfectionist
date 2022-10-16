using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private Transform pickUpTransform;
    [SerializeField] private Transform perfectTransform;
    private readonly List<Tween> moveToPerfectTweens = new();
    private GameObject thisObject;

    private void Awake()
    {
        var _object = GetComponent<NotPerfectObject>();
        _object.OnInitObject.AddListener(Init);
        _object.OnPickUpObject.AddListener(Pick);
        // _object.OnObjectFail.AddListener(Failed);
        _object.OnLastBehTapObject.AddListener(ToPerfect);
    }

    private void Init(GameObject _thisObject)
    {
        thisObject = _thisObject;
    }

    private void Pick()
    {
        thisObject.transform.DOMove(pickUpTransform.position, DataManager.Instance.balanceData.PickUpTime)
            .SetEase(Ease.InCubic);
    }

    private void ToPerfect()
    {
        moveToPerfectTweens.Add(thisObject.transform
            .DOMove(perfectTransform.position, DataManager.Instance.balanceData.PickUpTime).SetEase(Ease.InCubic));
    }

    private void Failed()
    {
        foreach (var _tween in moveToPerfectTweens)
            _tween.Kill();
    }
}