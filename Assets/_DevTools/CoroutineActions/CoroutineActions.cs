using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/* V1.0
Use cases:
1. Make second method, until first condition is true (every frame)
CoroutineActions.DoActionUntilConditionIsTrue(
            () => !m_someList.TrueForAll(x => x == null),
            () => TryUpdateMessage(m_someTMProText,$"All enemies is dead"));

2. (Wait until event(condition) will be true, and calls second action once)
CoroutineActions.WaitForConditionAndDoAction(
           () => m_someList.FindAll(x => x == null).Count == 3,
           () =>
           {
               ShowMessage($"All 3 targets are destroyed!", 0.5f);
               UITransactionsManager.Instance.StartRecolor($"MessagePanel", false, 0.5f);
           });

3. (just change any value over time)
CoroutineActions.ChangeFloat(x => Camera.main.orthographicSize = x, 20f, 90f, 1f, 2f);

4. Call any method after specified delay
CoroutineActions.ExecuteAction(1f, () => Debug.Log("Hello!"));
*/

public class CoroutineActions : MonoBehaviour
{
    private void Initialize()
    {
        enabled = true;
    }

    public static void DoActionUntilConditionIsTrue(Func<bool> condition, UnityAction action)
    {
        Instance.StartCoroutine(Instance.DoActionUntilConditionIsTrueCoroutine(condition, action));
    }

    private IEnumerator DoActionUntilConditionIsTrueCoroutine(Func<bool> condition, UnityAction action)
    {
        while (condition())
        {
            action();
            yield return new WaitForEndOfFrame();
        }
    }

    public static void WaitForConditionAndDoAction(Func<bool> condition, UnityAction action)
    {
        Instance.StartCoroutine(Instance.WaitForConditionAndDoActionCoroutine(condition, action));
    }

    private IEnumerator WaitForConditionAndDoActionCoroutine(Func<bool> condition, UnityAction action)
    {
        while (!condition()) yield return new WaitForEndOfFrame();
        action();
    }

    public static void ChangeFloat(UnityAction<float> action, float from, float to, float delayBefore, float time,
        bool isInstaSet = false)
    {
        Instance.StartCoroutine(Instance.ChangeFloatCoroutine(action, from, to, delayBefore, time, isInstaSet));
    }

    private IEnumerator ChangeFloatCoroutine(UnityAction<float> action, float from, float to, float delay, float time,
        bool isInstaSet)
    {
        var value = from;
        if (isInstaSet)
            action(value);
        yield return new WaitForSeconds(delay);

        var timer = 0f;
        var percent = 0f;
        while (timer < time)
        {
            action(percent * (to - from) + from);
            timer += Time.deltaTime;
            percent = timer / time;
            yield return new WaitForEndOfFrame();
        }

        action(to);
    }

    //public static void TryUpdateMessage(TMPro.TextMeshProUGUI text, string phrase)
    //{
    //    if (phrase != text.text)
    //        text.text = phrase;
    //}
    //public static void ShowMessage(TMPro.TextMeshProUGUI text, string phrase, float duration)
    //{
    //    text.gameObject.SetActive(true);
    //    text.text = phrase;
    //    if (duration > 0f)
    //        ExecuteAction(duration, () => text.gameObject.SetActive(false));
    //}

    public static void ExecuteAction(float delay, UnityAction action)
    {
        Instance.StartCoroutine(Instance.ExecuteActionCoroutine(delay, action));
    }

    private IEnumerator ExecuteActionCoroutine(float delay, UnityAction action)
    {
        yield return new WaitForSeconds(delay);
        action();
    }

    #region Singleton Init

    private static CoroutineActions _instance;

    private void Awake() // Init in order
    {
        if (_instance == null)
            Init();
        else if (_instance != this)
            Destroy(gameObject);
    }

    public static CoroutineActions Instance // Init not in order
    {
        get
        {
            if (_instance == null)
                Init();
            return _instance;
        }
        private set => _instance = value;
    }

    private static void Init() // Init script
    {
        _instance = FindObjectOfType<CoroutineActions>();
        if (_instance != null)
            _instance.Initialize();
    }

    #endregion
}