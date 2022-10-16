using System;
using UnityEngine;

public class ObjectCollider : MonoBehaviour
{
    private Action OnObstacleCollised;
    private Rigidbody rigidbody;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
            if (collision.transform.GetComponent<ObstacleObject>())
            {
                Debug.Log("SSS");
                rigidbody.isKinematic = false;
                OnObstacleCollised.Invoke();
            }
    }

    public void Init(Action _action)
    {
        rigidbody = GetComponent<Rigidbody>();
        OnObstacleCollised = _action;
    }
}