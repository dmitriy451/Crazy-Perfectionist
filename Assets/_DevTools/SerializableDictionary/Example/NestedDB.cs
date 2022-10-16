using System;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

namespace RotaryHeart.Lib
{
    [CreateAssetMenu(fileName = "NestedDB.asset", menuName = "Nested DB")]
    public class NestedDB : ScriptableObject
    {
        [SerializeField] [ID("id")] public MainDict nested;
    }

    [Serializable]
    public class Example
    {
        public string id;
        public QueryTriggerInteraction enumVal;
        public NestedDict nestedData;
    }

    [Serializable]
    public class NestedExample
    {
        public GameObject prefab;
        public float speed;
        public Color color;
        public Nested2Dict deepNested;
    }

    [Serializable]
    public class MainDict : SerializableDictionaryBase<string, Example>
    {
    }

    [Serializable]
    public class NestedDict : SerializableDictionaryBase<int, NestedExample>
    {
    }

    [Serializable]
    public class Nested2Dict : SerializableDictionaryBase<QueryTriggerInteraction, string>
    {
    }
}