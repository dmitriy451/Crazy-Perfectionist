using System.Collections;
using UnityEngine;

namespace RH.Utilities.Processes.Executing
{
    public class SerialProcessExecutor : MonoBehaviour
    {
        public void ExecuteProcess(IEnumerator process)
        {
            StartCoroutine(process);
        }

        public void KillProcess(IEnumerator process)
        {
            StopCoroutine(process);
        }

        public void KillAllProcesses()
        {
            StopAllCoroutines();
        }
    }
}