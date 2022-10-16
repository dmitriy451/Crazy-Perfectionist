using UnityEngine;
using UnityEngine.Events;

public static class InputEvents
{
    public static UnityEvent<Vector2> FingerTap = new();
    public static UnityEvent<Vector2> FingerDown = new();
    public static UnityEvent<Vector2> FingerUp = new();
}