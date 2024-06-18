using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField]
    private SaveSetup _saveSetup;

    public Material standardSkin;

    private string _path = Application.dataPath + "/save.txt";

    public int lastLevel;
    public int nextLevel;

    public Action<SaveSetup> FileLoaded;

    public SaveSetup Setup
    {
        get { return _saveSetup; }
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public bool CheckFile()
    {
        return File.Exists(_path);
    }

    public void CreateNewSave()
    {
        if (CheckFile())
        {
            File.Delete(_path);
        }

        _saveSetup = new SaveSetup();
        _saveSetup.currentLevel = 1;
        _saveSetup.LevelEnded = true;
        _saveSetup.gameStarted = false;
        _saveSetup.nextLevel = 2;
        _saveSetup.clothSetup.clothType = 0;
        _saveSetup.clothSetup.texture = (Texture2D)standardSkin.GetTexture("_EmissionMap");
        _saveSetup.coins = 0;
        _saveSetup.potion = 0;
        _saveSetup.PlayerName = "Thiago";

    }

    private void Start()
    {
        Invoke(nameof(Load), 1f);
    }


    #region SAVE
    public void Save()
    {
        string setupToJson = JsonUtility.ToJson(_saveSetup, true);
        Debug.Log(setupToJson);
        SaveFile(setupToJson);
    }

    public void SaveItems()
    {
        _saveSetup.coins = Itens.ItemManager.Instance.GetItemByType(Itens.ItemType.COIN).soInt.value;
        _saveSetup.potion = Itens.ItemManager.Instance.GetItemByType(Itens.ItemType.LIFE_PACK).soInt.value;
        Save();
    }

    public void SaveCloth()
    {
        _saveSetup.clothSetup = Player.Instance.getCloth();
        Save();
    }

    public void SaveName(string text)
    {
        _saveSetup.PlayerName = text;
        Save();
    }
    public void SaveCurrentLevel()
    {
        _saveSetup.gameStarted = true;
        _saveSetup.LevelEnded = false;
        _saveSetup.nextLevel = _saveSetup.currentLevel + 1;
        _saveSetup.checkpoint = CheckpointManager.Instance.GetPositionFromLastCheckpoint();
        SaveCloth();
        SaveItems();
        Save();
    }
    public void LevelFinished()
    {
        _saveSetup.gameStarted = true;
        _saveSetup.currentLevel += _saveSetup.currentLevel;
        _saveSetup.LevelEnded = true;
        _saveSetup.nextLevel = _saveSetup.currentLevel + 1;
        SaveCloth();
        SaveItems();
        Save();
    }


    #endregion
    private void SaveFile(string json)
    {
        Debug.Log(_path);
        File.WriteAllText(_path, json);
    }

    [NaughtyAttributes.Button]
    private void Load()
    {
        string fileLoaded = "";

        if (CheckFile())
        {
            fileLoaded = File.ReadAllText(_path);
            _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);
            lastLevel = _saveSetup.currentLevel;
        } else
        {
            CreateNewSave();
            Save();
        }

        FileLoaded.Invoke(_saveSetup);
    }

}

[System.Serializable]
public class SaveSetup
{
    public bool gameStarted = false;
    public int currentLevel;
    public int nextLevel;
    public bool LevelEnded = true;
    public float coins;
    public float potion;
    public Cloth.ClothSetup clothSetup = new Cloth.ClothSetup();
    public Vector3 checkpoint;

    public string PlayerName;
}
