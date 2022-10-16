using UnityEngine;

[RequireComponent(typeof(NotPerfectObject))]
public class ObjectParticleManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem landingPS;
    [SerializeField] private ParticleSystem successPS;
    [SerializeField] private ParticleSystem failPS;

    private void Awake()
    {
        var _notPerfectObject = GetComponent<NotPerfectObject>();
        _notPerfectObject.OnReadyToCheckObject.AddListener(() =>
            CoroutineActions.ExecuteAction(DataManager.Instance.balanceData.PickUpTime, () => landingPS.Play()));
        _notPerfectObject.OnCompleteObject.AddListener(_x =>
        {
            if (_x > DataManager.Instance.balanceData.ThresholdForPassingLevel) successPS.Play();
            else failPS.Play();
        });
    }
}