using System;

namespace RotaryHeart.Lib.SerializableDictionary
{
    /// <summary>
    ///     Attribute used to force drawing a key as a property
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class DrawKeyAsPropertyAttribute : Attribute
    {
    }
}