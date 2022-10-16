using System;

namespace RotaryHeart.Lib.SerializableDictionary
{
    [AttributeUsage(AttributeTargets.Field)]
    public class IDAttribute : Attribute
    {
        /// <summary>
        ///     Serializable field name for the property id
        /// </summary>
        /// <param name="id">Field name</param>
        public IDAttribute(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}