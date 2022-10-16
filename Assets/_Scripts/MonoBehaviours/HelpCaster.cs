using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HelpCaster : MonoBehaviour
{
    [SerializeField] private Material helpMaterial;
    [SerializeField] private Transform pickUpTransform;
    [SerializeField] private bool isPickUpTransform;
    private readonly List<Tween> flashingTweens = new();
    private GameObject helpObject;
    private MatchChecker matchChecker;

    private readonly List<Material> mats = new();
    private NotPerfectObject notPerfectObject;
    private Transform savedPerfectTransform;

    private void Awake()
    {
        notPerfectObject = GetComponent<NotPerfectObject>();
        matchChecker = GetComponent<MatchChecker>();
        notPerfectObject.OnStartObject.AddListener(CreateHelpObject);
        notPerfectObject.OnReadyToCheckObject.AddListener(HideHelpObject);
        matchChecker.OnSavePerfetObjectTransform.AddListener(SavePerfectTransform);
    }

    private void CreateHelpObject()
    {
        helpObject = Instantiate(notPerfectObject.thisObject);
        helpObject.transform.SetParent(notPerfectObject.objectParent);
        //while objects only rotate - pickUpTransform, else savedPerfectTransform
        helpObject.transform.position = isPickUpTransform ? pickUpTransform.position : savedPerfectTransform.position;
        //helpObject.transform.position = new Vector3(helpObject.transform.position.x, helpObject.transform.position.y, helpObject.transform.position.z);
        helpObject.transform.rotation = savedPerfectTransform.rotation;
        helpObject.transform.localScale = savedPerfectTransform.localScale;
        helpObject.transform.localScale = new Vector3(helpObject.transform.localScale.x,
            helpObject.transform.localScale.y, helpObject.transform.localScale.z); // y = 0.01f

        var _mrs = helpObject.GetComponentsInChildren<MeshRenderer>();

        for (var u = 0; u < _mrs.Length; u++)
        {
            mats.AddRange(_mrs[u].materials);
            for (var i = 0; i < mats.Count; i++)
            {
                mats[i] = helpMaterial;
                mats[i].mainTexture = null;
            }

            _mrs[u].materials = mats.ToArray();
            _mrs[u].GetComponent<Collider>().enabled = false;
        }

        StartFlashing();
    }

    private void HideHelpObject()
    {
        StopFlashing();
        helpObject.SetActive(false);
    }

    private void SavePerfectTransform(Transform _t)
    {
        savedPerfectTransform = _t;
    }

    private void StartFlashing()
    {
        foreach (var _mat in mats)
            flashingTweens.Add(_mat.DOFade(0.2f, "_BaseColor", 1.0f)
                .ChangeStartValue(new Color(0.2447335f, 0.415056f, 0.1703089f, 0.0f)).SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.Linear));
    }

    private void StopFlashing()
    {
        foreach (var _flashingTween in flashingTweens)
            _flashingTween.Kill();

        //foreach (var _mat in mats)
        //    _mat.SetFloat("_BaseColor", 0.0f);
    }
}