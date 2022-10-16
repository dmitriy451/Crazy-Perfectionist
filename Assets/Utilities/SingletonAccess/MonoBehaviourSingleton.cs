using UnityEngine;

namespace RH.Utilities.SingletonAccess
{
    public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    var instancies = (T[])FindObjectsOfType(typeof(T));

                    if (instancies.Length == 0)
                    {
                        Debug.LogError($"Нет ни одного экземпляра {typeof(T)} на сцене!");
                        return null;
                    }

                    if (instancies.Length > 1)
                    {
                        Debug.LogError($"На сцене больше одного экземпляра {typeof(T)}");
                        return null;
                    }

                    instance = instancies[0];
                    return instance;
                }

                return instance;
            }
        }

        public static bool IsInstanceExist => instance == null ? false : true;

        private void Awake()
        {
            if (instance != this && instance != null)
            {
                Destroy(this);
            }
            else
            {
                var instancies = (T[])FindObjectsOfType(typeof(T));

                if (instancies.Length == 0)
                    Debug.LogError($"Нет ни одного экземпляра {typeof(T)} на сцене!");
                else if (instancies.Length > 1)
                    Debug.LogError($"На сцене больше одного экземпляра {typeof(T)}");
                else
                    instance = instancies[0];
            }
        }
    }
}