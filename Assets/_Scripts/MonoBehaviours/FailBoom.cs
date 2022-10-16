using NaughtyAttributes;
using UnityEngine;

public class FailBoom : MonoBehaviour
{
    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        GlobalEvents.OnLevelFailBoom?.AddListener(Boom);
    }

    [Button]
    public void Boom()
    {
        rigidbody.isKinematic = false;
        rigidbody.AddExplosionForce(3000.0f, new Vector3(0, -1, 1) * 3.0f, 100.0f, 100.0f);
    }
}