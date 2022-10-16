using Lean.Touch;
using RH.Utilities.ComponentSystem;

public class LeanTouchSystem : BaseSystem
{
    protected override void Init()
    {
        LeanTouch.OnFingerTap += HandleFingerTap;
        LeanTouch.OnFingerDown += HandleFingerDown;
        LeanTouch.OnFingerUp += HandleFingerUp;
    }

    public override void Dispose()
    {
        LeanTouch.OnFingerTap -= HandleFingerTap;
        LeanTouch.OnFingerDown -= HandleFingerDown;
        LeanTouch.OnFingerUp -= HandleFingerUp;
    }

    private void HandleFingerTap(LeanFinger finger)
    {
        //currentSportsmanBehaviour?.OnTap?.Invoke();
        InputEvents.FingerTap?.Invoke(finger.ScreenPosition);
        //Debug.Log("You just tapped the screen with finger " + finger.Index + " at " + finger.ScreenPosition);
    }

    private void HandleFingerDown(LeanFinger finger)
    {
        //currentSportsmanBehaviour?.OnDown?.Invoke();
        InputEvents.FingerDown?.Invoke(finger.ScreenPosition);
    }

    private void HandleFingerUp(LeanFinger finger)
    {
        //currentSportsmanBehaviour?.OnUp?.Invoke();
        InputEvents.FingerUp?.Invoke(finger.ScreenPosition);
        //Debug.Log("You just tapped the screen with finger " + finger.Index + " at " + finger.ScreenPosition);
    }
}