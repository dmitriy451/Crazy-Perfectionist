using UnityEngine;

public class ObstacleObject : MonoBehaviour
{
    private Collider collider;
    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    public void Enable()
    {
        collider.enabled = true;
        rigidbody.isKinematic = false;
    }

    public void Disable()
    {
        collider.enabled = false;
        rigidbody.isKinematic = true;
    }

    public void FailRigidbodyActivate()
    {
        collider.enabled = true;
        rigidbody.isKinematic = false;
    }
}