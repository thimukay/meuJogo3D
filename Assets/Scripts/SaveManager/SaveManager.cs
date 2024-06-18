using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
   private void Save()
    {
        SaveSetup setup = new SaveSetup();
        setup.lastLevel = 2;
        setup.PlayerName = "Thiago";

        string setupToJson = JsonUtility.ToJson(setup, true);
        Debug.Log(setupToJson);
        SaveFile(setupToJson);
    }

    private void SaveFile(string json)
    {
        string path = Application.dataPath + "/save.txt";

        //string fileLoaded = "";
        //if (File.Exists(path)) fileLoaded = File.ReadAllText(path);
        Debug.Log(path);
        File.WriteAllText(path, json);
    }

}

[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public string PlayerName;
}
