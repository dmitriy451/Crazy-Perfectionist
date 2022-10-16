using System.Collections;
using UnityEngine;

namespace RH.Utilities.Coroutines
{
    public static class CoroutineLauncher
    {
        private static CoroutinesLauncherReciever _reciever;

        private static CoroutinesLauncherReciever reciever
        {
            get
            {
                if (_reciever == null)
                    _reciever = new GameObject("CoroutinesLauncher", typeof(CoroutinesLauncherReciever))
                        .GetComponent<CoroutinesLauncherReciever>();

                return _reciever;
            }
        }

        public static bool Exist => _reciever != null;

        public static Coroutine Start(IEnumerator coroutine)
        {
            return reciever.StartCoroutine(coroutine);
        }

        public static void Stop(IEnumerator coroutine)
        {
            reciever.StopCoroutine(coroutine);
        }

        public static void Stop(Coroutine coroutine)
        {
            reciever.StopCoroutine(coroutine);
        }

        public static void StopAll()
        {
            reciever.StopAllCoroutines();
        }

        private class CoroutinesLauncherReciever : MonoBehaviour
        {
        }
    }
}