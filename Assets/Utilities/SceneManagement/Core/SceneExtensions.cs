using System;
using UnityEngine.SceneManagement;

namespace RH.Utilities.SceneManagement.Core
{
    public static class SceneExtensions
    {
        public static ISceneReciever GetSceneReciever(this Scene scene)
        {
            foreach (var item in scene.GetRootGameObjects())
            {
                var reciever = item.GetComponent<ISceneReciever>();
                if (reciever != null)
                    return reciever;
            }

            throw new Exception($"Scene {scene.name} doesn't contains SceneReciever");
        }
    }
}