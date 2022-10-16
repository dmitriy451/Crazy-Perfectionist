using System.IO;
using UnityEngine;

public abstract class BaseData : ScriptableObject
{
    public abstract void ResetData();

    public void SaveData()
    {
        var json = JsonUtility.ToJson(this);
        Debug.Log(json);
        var _filename = $"{GetType()}.json";
        var _dataPath = Path.Combine(Utility.GetDataPath(), _filename);
        if (!Directory.Exists(Utility.GetDataPath()))
            Directory.CreateDirectory(Utility.GetDataPath());
        File.WriteAllText(_dataPath, json);
    }

    public void LoadData()
    {
        var _filename = $"{GetType()}.json";
        var _dataPath = Path.Combine(Utility.GetDataPath(), _filename);
        if (File.Exists(_dataPath))
        {
            var json = File.ReadAllText(_dataPath);
            Debug.Log(json);
            JsonUtility.FromJsonOverwrite(json, this);
        }
    }
}