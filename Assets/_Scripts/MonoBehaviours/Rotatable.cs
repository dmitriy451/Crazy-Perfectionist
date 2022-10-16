using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Rotatable : NotPerfectObjectBehaviour
{
    [SerializeField] public bool isStepRotate = true;
    [SerializeField] private float rotatingDegree;
    [SerializeField] private bool isYRotate;
    [SerializeField] private bool isXRotate;
    [SerializeField] private bool isZRotate;
    [SerializeField] public bool isMirror;

    private readonly List<Tween> tweens = new();

    public override void Randomize()
    {
        Debug.Log("Randomize()");
        if (isYRotate)
        {
            var _rand = Random.Range(1, 360 / rotatingDegree);
            thisObject.transform.localRotation =
                Quaternion.Euler(0, Random.Range(rotatingDegree, rotatingDegree * _rand), 0);
        }

        if (isXRotate)
        {
            var _rand = Random.Range(1, 360 / rotatingDegree);
            thisObject.transform.localRotation =
                Quaternion.Euler(Random.Range(rotatingDegree, rotatingDegree * _rand), 0, 0);
        }

        if (isZRotate)
        {
            var _rand = Random.Range(1, 360 / rotatingDegree);
            thisObject.transform.localRotation =
                Quaternion.Euler(0, 0, Random.Range(rotatingDegree, rotatingDegree * _rand));
        }
    }

    public override void RunBehaviour()
    {
        isStarted = true;
        if (isStepRotate)
            StartCoroutine(StartStepRotate());
        else
            StartSmoothRotate();
    }

    public override void Tap()
    {
        isStarted = false;
        foreach (var _tween in tweens)
            _tween.Kill();
    }

    public override void Failed()
    {
        isStarted = false;
        foreach (var _tween in tweens)
            _tween.Kill();
    }

    private void StartSmoothRotate()
    {
        if (isYRotate)
            tweens.Add(thisObject.transform.DOBlendableLocalRotateBy(new Vector3(0, 360, 0),
                    actionTime * DataManager.Instance.balanceData.RotatingSmoothSpeed, RotateMode.LocalAxisAdd)
                .SetLoops(-1)
                .SetEase(Ease.Linear));

        if (isXRotate)
            tweens.Add(thisObject.transform.DOBlendableLocalRotateBy(new Vector3(360, 0, 0),
                    actionTime * DataManager.Instance.balanceData.RotatingSmoothSpeed, RotateMode.LocalAxisAdd)
                .SetLoops(-1)
                .SetEase(Ease.Linear));

        if (isZRotate)
            tweens.Add(thisObject.transform.DOBlendableLocalRotateBy(new Vector3(0, 0, 360),
                    actionTime * DataManager.Instance.balanceData.RotatingSmoothSpeed, RotateMode.LocalAxisAdd)
                .SetLoops(-1)
                .SetEase(Ease.Linear));
    }

    private IEnumerator StartStepRotate()
    {
        yield return new WaitForSeconds(actionTime * DataManager.Instance.balanceData.RotatingStepSpeed + 0.25f);

        if (isStarted)
        {
            if (isYRotate)
                tweens.Add(thisObject.transform.DOLocalRotate(new Vector3(0, rotatingDegree, 0),
                        actionTime * DataManager.Instance.balanceData.RotatingStepSpeed, RotateMode.LocalAxisAdd)
                    .SetEase(Ease.InCubic)); //Ease.InOutElastic

            if (isXRotate)
                tweens.Add(thisObject.transform.DOLocalRotate(new Vector3(rotatingDegree, 0, 0),
                        actionTime * DataManager.Instance.balanceData.RotatingStepSpeed, RotateMode.LocalAxisAdd)
                    .SetEase(Ease.InCubic));

            if (isZRotate)
                tweens.Add(thisObject.transform.DOLocalRotate(new Vector3(0, 0, rotatingDegree),
                        actionTime * DataManager.Instance.balanceData.RotatingStepSpeed, RotateMode.LocalAxisAdd)
                    .SetEase(Ease.InCubic));

            StartCoroutine(StartStepRotate());
        }
    }
}