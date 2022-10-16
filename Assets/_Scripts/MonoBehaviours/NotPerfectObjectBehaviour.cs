using UnityEngine;

[RequireComponent(typeof(NotPerfectObject))]
public abstract class NotPerfectObjectBehaviour : MonoBehaviour
{
    [SerializeField] public int runQueueNum;
    [SerializeField] protected float actionTime;
    [SerializeField] protected bool isRandomize = true;

    protected bool isStarted;
    protected GameObject thisObject;

    private void Awake()
    {
        var _object = GetComponent<NotPerfectObject>();
        _object.OnInitObject.AddListener(Init);
        _object.OnObjectFail.AddListener(Failed);
        _object.OnRandomizeObject.AddListener(() =>
        {
            if (isRandomize) Randomize();
        });
    }

    public virtual void Init(GameObject _thisObject)
    {
        thisObject = _thisObject;
    }

    public abstract void Randomize();
    public abstract void RunBehaviour();
    public abstract void Tap();
    public abstract void Failed();
}