using UnityEngine;

namespace RH.Utilities.ResourcesLoading
{
    public static class ResourcesLoader
    {
        private static readonly BaseResourcesFactory _resourcesFactory;

        static ResourcesLoader()
        {
            _resourcesFactory = new BaseResourcesFactory();
        }

        public static T Get<T>(string name) where T : Object
        {
            return _resourcesFactory.Get<T>(name);
        }

        public static T Instantiate<T>(string name, Transform parent = null) where T : Component
        {
            return _resourcesFactory.Instantiate<T>(name, parent);
        }

        public static T Instantiate<T>(string name, Vector3 position, Transform parent = null) where T : Component
        {
            var loadedObject = Instantiate<T>(name, parent);
            loadedObject.transform.position = position;

            return loadedObject;
        }
    }
}