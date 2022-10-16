using System.Collections.Generic;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

public class EditorDatabase : MonoBehaviour
{
    public static bool IsInited = false;

#if UNITY_EDITOR
    [Header("Auto")] [Required] public Object levelsFolder;
#endif

    [Header("Data")] public List<GameObject> levelDatas;

    private void Initialize()
    {
        // Init data here
        enabled = true;
    }

#if UNITY_EDITOR
    [Button]
    private void FillData()
    {
        //levelDatas = AssetLoader<GameObject>.GetItems(levelsFolder);


#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }
#endif

    #region Singleton Init

    private static EditorDatabase _instance;

    private void Awake() // Init in order
    {
        if (_instance == null)
        {
            Init();
        }
        else if (_instance != this)
        {
            Debug.Log($"Destroying {gameObject.name}, caused by one singleton instance");
            Destroy(gameObject);
        }
    }

    public static EditorDatabase Instance // Init not in order
    {
        get
        {
            if (_instance == null)
                Init();
            return _instance;
        }
        private set => _instance = value;
    }

    private static void Init() // Init script
    {
        _instance = FindObjectOfType<EditorDatabase>();
        if (_instance != null)
            _instance.Initialize();
    }

    #endregion
}