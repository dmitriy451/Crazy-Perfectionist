using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : UIElement
{
    [HideInInspector] public UnityEvent OnHidePanel;
    [HideInInspector] public UnityEvent OnShowPanel;
    protected bool panelIsOpen;

    private void Start()
    {
        Init();
    }

    protected abstract void Init();

    protected override void
        SetShow(bool _isShow) // [System.Runtime.CompilerServices.CallerMemberName] string memberName = "" - WHO
    {
        base.SetShow(_isShow);
        if (_isShow)
        {
            OnShowPanel.Invoke();
            panelIsOpen = true;
        }
        else
        {
            OnHidePanel.Invoke();
            panelIsOpen = false;
        }
    }
}