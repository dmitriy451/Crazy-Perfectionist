using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [SerializeField] private GameObject prefabObject;
    [SerializeField] private int poolDepth = 1;
    [SerializeField] private bool canGrow;

    private readonly List<GameObject> pool = new();

    private void Awake()
    {
        for (var i = 0; i < poolDepth; i++)
            CreatePoolObject();
    }

    public GameObject GetAvaliableObject()
    {
        for (var i = 0; i < pool.Count; i++)
            if (!pool[i].activeInHierarchy)
                return pool[i];

        if (canGrow)
            return CreatePoolObject();

        return null;
    }

    private GameObject CreatePoolObject()
    {
        var _pooledObject = Instantiate(prefabObject);
        _pooledObject.SetActive(false);
        pool.Add(_pooledObject);
        return _pooledObject;
    }
}