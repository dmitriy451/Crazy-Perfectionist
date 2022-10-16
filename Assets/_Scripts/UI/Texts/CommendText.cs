using System.Collections.Generic;
using UnityEngine;

public class CommendText : BaseText
{
    [SerializeField] private List<string> commendWords;

    protected override void Init()
    {
        UIEvents.ShowCommend.AddListener(ShowCommend);
    }

    private void ShowCommend()
    {
        UpdateText(commendWords[Random.Range(0, commendWords.Count)]);
        animator.SetTrigger("IsCommend");
    }
}