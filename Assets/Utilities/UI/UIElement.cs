using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class UIElement : MonoBehaviour
{
    protected Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void SetShow(bool _isShow)
    {
        if (_isShow)
            Show();
        else
            Hide();
    }

    private void Show()
    {
        animator.SetBool("IsShow", true);
    }

    private void Hide()
    {
        animator.SetBool("IsShow", false);
    }
}