using UnityEngine;

namespace RH.Utilities.ResourcesLoading
{
    public interface IAssetsFactory
    {
        T Instantiate<T>(string name, Transform parent) where T : Component;
        T Get<T>(string name) where T : Object;
    }
}