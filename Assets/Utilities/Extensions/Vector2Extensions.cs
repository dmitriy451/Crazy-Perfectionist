using UnityEngine;

namespace RH.Utilities.Extensions
{
    public static class Vector2Extensions
    {
        public static bool Approximately(this Vector2 a, Vector2 b)
        {
            return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y);
        }
    }
}