using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(MatchChecker))]
public class Movable : NotPerfectObjectBehaviour
{
    [Slider(0.1f, 0.5f)] [SerializeField] private float range;

    [SerializeField] private bool isXMoving;
    [SerializeField] private bool isZMoving;

    private readonly List<Tween> tweens = new();

    private float xRandom;
    private float zRandom;

    public override void Randomize()
    {
        var _rand = Random.Range(0.1f, range);
        if (isXMoving)
        {
            xRandom = Random.Range(-_rand, _rand);
            thisObject.transform.localPosition = new Vector3(xRandom, 0, 0);
        }

        if (isZMoving)
        {
            zRandom = Random.Range(-_rand, _rand);
            thisObject.transform.localPosition = new Vector3(thisObject.transform.position.x, 0, zRandom);
        }
    }

    public override void RunBehaviour()
    {
        isStarted = true;

        if (isXMoving)
            tweens.Add(thisObject.transform
                .DOLocalMoveX(-xRandom + -xRandom, actionTime * DataManager.Instance.balanceData.MovingSpeed)
                .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear));

        if (isZMoving)
            tweens.Add(thisObject.transform
                .DOLocalMoveZ(-zRandom + -xRandom, actionTime * DataManager.Instance.balanceData.MovingSpeed)
                .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear));
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
}