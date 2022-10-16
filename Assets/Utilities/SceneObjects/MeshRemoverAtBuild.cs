using UnityEngine;

namespace RH.Utilities.SceneObjects
{
    public class MeshRemoverAtBuild : MonoBehaviour
    {
        private void Awake()
        {
            if (!Application.isEditor)
            {
                Destroy(GetComponent<MeshFilter>());
                Destroy(GetComponent<MeshRenderer>());
                Destroy(this);
            }
        }
    }
}