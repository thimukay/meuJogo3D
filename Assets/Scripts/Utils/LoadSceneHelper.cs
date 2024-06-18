using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneHelper : MonoBehaviour
{
    public void LoadLevel()
    {
        if (SaveManager.Instance.Setup.gameStarted)
        {
            SceneManager.LoadScene(SaveManager.Instance.Setup.currentLevel);
        }
    }

    public void LoadFirstLevel()
    {
            SaveManager.Instance.CreateNewSave();
            SaveManager.Instance.Save();
            SceneManager.LoadScene(1);
    }
}
