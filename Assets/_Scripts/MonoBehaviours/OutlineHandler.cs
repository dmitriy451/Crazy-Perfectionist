using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(NotPerfectObject))]
public class OutlineHandler : MonoBehaviour
{
    [SerializeField] private Transform parentOutlineObject;

    private readonly List<Material> mats = new();
    private readonly List<Tween> outlineWidthTweens = new();

    private void Awake()
    {
        var _mrs = new List<MeshRenderer>();
        _mrs.AddRange(parentOutlineObject.GetComponentsInChildren<MeshRenderer>());

        for (var u = 0; u < _mrs.Count; u++)
            mats.AddRange(_mrs[u].materials);

        var notPerfectObject = GetComponent<NotPerfectObject>();
        notPerfectObject.OnInitObject.AddListener(x => OutlineStartFlashing());
        notPerfectObject.OnObjectCanRetry.AddListener(OutlineStartFlashing);
        notPerfectObject.OnPickUpObject.AddListener(OutlineStopFlashing);
    }

    private void OutlineStartFlashing()
    {
        foreach (var _mat in mats)
            outlineWidthTweens.Add(_mat.DOFloat(1.3f, "_OutlineWidth", 0.5f).ChangeStartValue(0.0f)
                .SetLoops(-1, LoopType.Yoyo));
    }

    private void OutlineStopFlashing()
    {
        foreach (var _outlineWidthTween in outlineWidthTweens)
            _outlineWidthTween.Kill();

        foreach (var _mat in mats)
            _mat.SetFloat("_OutlineWidth", 0.0f);
    }
}