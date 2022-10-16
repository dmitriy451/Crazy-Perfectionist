using System.IO;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] public MainData mainData;
    [SerializeField] public BalanceData balanceData;
    [SerializeField] public SettingsData settingsData;

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
#if UNITY_EDITOR
        //DeleteAllGameData(); // TODO: REMOVE
#endif
    }

    protected override void Initialize()
    {
        Debug.Log(Utility.GetDataPath());
        LoadData();
    }

    private void SaveData()
    {
        mainData.SaveData();
        settingsData.SaveData();
    }

    private void LoadData()
    {
        mainData.LoadData();
        settingsData.LoadData();
    }

    [Button]
    public void ResetData()
    {
        mainData.ResetData();
    }

#if UNITY_EDITOR
    [ExecuteInEditMode]
    [MenuItem("Tools/DeleteAllGameData")]
    public static void DeleteAllGameData()
    {
        if (Directory.Exists(Utility.GetDataPath()))
            Directory.Delete(Utility.GetDataPath(), true);
    }
#endif
}