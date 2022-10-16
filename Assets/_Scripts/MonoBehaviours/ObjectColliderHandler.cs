using System;
using UnityEngine;

[RequireComponent(typeof(NotPerfectObject))]
public class ObjectColliderHandler : MonoBehaviour
{
    private NotPerfectObject notPerfectObject;
    private ObjectCollider objectCollider;

    private void Awake()
    {
        notPerfectObject = GetComponent<NotPerfectObject>();
        Action _onObstacleCollised = () => { notPerfectObject.OnObjectFail.Invoke(); };
        objectCollider = GetComponentInChildren<ObjectCollider>();
        objectCollider.Init(_onObstacleCollised);
    }
}