using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(NotPerfectObject))]
public class MatchChecker : MonoBehaviour
{
    [SerializeField] private Transform perfectTransform;
    [HideInInspector] public UnityEvent<Transform> OnSavePerfetObjectTransform;
    private NotPerfectObject notPerfectObject;
    private GameObject thisObject;

    private void Awake()
    {
        notPerfectObject = GetComponent<NotPerfectObject>();
        notPerfectObject.OnReadyToCheckObject.AddListener(MatchCheck);
        notPerfectObject.OnInitObject.AddListener(SavePrefectTransform);
    }

    private void SavePrefectTransform(GameObject _thisObject)
    {
        thisObject = _thisObject;
        //perfectTransform.position = thisObject.transform.position;
        //perfectTransform.rotation = thisObject.transform.rotation;
        //perfectTransform.localScale = thisObject.transform.localScale;
        OnSavePerfetObjectTransform.Invoke(perfectTransform);
    }

    private void MatchCheck()
    {
        float _error = 0;
        var _behCounter = 0;

        if (GetComponent<Movable>())
        {
            thisObject.transform.position =
                new Vector3(thisObject.transform.position.x, 0.0f, thisObject.transform.position.z);
            perfectTransform.position = new Vector3(perfectTransform.position.x, 0.0f, perfectTransform.position.z);
            _error += Vector3.Distance(thisObject.transform.position, perfectTransform.position);
            _behCounter++;
        }

        var _rotatable = GetComponent<Rotatable>();
        if (_rotatable)
        {
            if (_rotatable.isStepRotate)
                _error += ApproximatelyQuaternions(thisObject.transform.rotation, perfectTransform.rotation, 0.0000004f)
                    ? 0.0f
                    : 1.0f;
            else
                _error += 1 - Mathf.Abs(Quaternion.Dot(thisObject.transform.rotation, perfectTransform.rotation));
            _behCounter++;

            if (_rotatable.isMirror && _error > 0.97f)
                _error = 0.0f;

            Debug.Log(
                $"thisObject.transform.rotation.eulerAngles: {thisObject.transform.rotation.eulerAngles}, perfectTransform.rotation.eulerAngles {perfectTransform.rotation.eulerAngles} = {thisObject.transform.rotation.eulerAngles == perfectTransform.rotation.eulerAngles}");
        }

        var _accuracy = (int)(100.0f - _error * 100.0f);

        if (_accuracy >= 99)
        {
            Help();
            notPerfectObject.MatchCheckComplete(100);
            return;
        }

        notPerfectObject.MatchCheckComplete(_accuracy);
    }

    private void Help()
    {
        //thisObject.transform.DOMove(perfectTransform.position, 0.15f);
        thisObject.transform.DORotateQuaternion(perfectTransform.rotation, 0.15f);
        //thisObject.transform.DOScale(perfectTransform.localScale, 0.15f);
    }

    public bool ApproximatelyQuaternions(Quaternion quatA, Quaternion value, float acceptableRange)
    {
        return 1 - Mathf.Abs(Quaternion.Dot(quatA, value)) < acceptableRange;
    }
}