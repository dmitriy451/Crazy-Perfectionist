using DG.Tweening;
using UnityEngine;

public class LevelCompleteJumpObject : MonoBehaviour
{
    [SerializeField] private bool isVertical;

    internal void Jump(int _queueNum)
    {
        CoroutineActions.ExecuteAction(_queueNum * 0.2f,
            () => { transform.DOPunchScale(Vector3.one * 0.03f, 0.2f, 1); });
    }
}