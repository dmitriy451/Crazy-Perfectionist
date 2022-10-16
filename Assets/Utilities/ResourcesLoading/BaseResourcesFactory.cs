using System.Collections.Generic;
using UnityEngine;

namespace RH.Utilities.ResourcesLoading
{
    public class BaseResourcesFactory : IAssetsFactory
    {
        private static readonly List<Object> _loadedObjects = new();

        public virtual T Instantiate<T>(string name, Transform parent) where T : Component
        {
            return Object.Instantiate(Get<T>(name), parent);
        }

        public virtual T Get<T>(string name) where T : Object
        {
            if (IsLoaded(name))
                return GetLoadedObject<T>(name);
            return Load<T>(name);
        }

        private bool IsLoaded(string name)
        {
            return GetLoadedObject<Object>(name) != null;
        }

        private T GetLoadedObject<T>(string name) where T : Object
        {
            return _loadedObjects.Find(x => x.name.Equals(name)) as T;
        }

        private T Load<T>(string name) where T : Object
        {
            var resource = Resources.Load<T>(name);
            _loadedObjects.Add(resource);
            return resource;
        }
    }
}