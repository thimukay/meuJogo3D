using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class SaveManager : Singleton<SaveManager>
{
    private SaveSetup _saveSetup;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 2;
        _saveSetup.PlayerName = "Thiago";
    }


    #region SAVE
    private void Save()
    {
        string setupToJson = JsonUtility.ToJson(_saveSetup, true);
        Debug.Log(setupToJson);
        SaveFile(setupToJson);
    }
    
    public void SaveName(string text)
    {
        _saveSetup.PlayerName = text;
        Save();
    }
    public void SaveLastLevel(int level)
    {
        _saveSetup.lastLevel = level;
        Save();
    }
    #endregion

    private void SaveFile(string json)
    {
        string path = Application.dataPath + "/save.txt";

        //string fileLoaded = "";
        //if (File.Exists(path)) fileLoaded = File.ReadAllText(path);
        Debug.Log(path);
        File.WriteAllText(path, json);
    }

    [NaughtyAttributes.Button]
    private void SaveLevelOne()
    {
        SaveLastLevel(1);
    }

}

[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public string PlayerName;
}
