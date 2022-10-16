using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(NotPerfectObject))]
public class AnimatorController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        var _notPerfectObject = GetComponent<NotPerfectObject>();
        _notPerfectObject.OnReadyToCheckObject.AddListener(() =>
            CoroutineActions.ExecuteAction(DataManager.Instance.balanceData.PickUpTime, () => Landing()));
    }

    [Button]
    public void Landing()
    {
        animator.SetTrigger("IsLanding");
    }
}