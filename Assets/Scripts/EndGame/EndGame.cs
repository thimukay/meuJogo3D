using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndGame : MonoBehaviour
{

    public List<GameObject> EndgameObjects;
    public LoadSceneHelper lch;

    private bool _endGame = false;

    public int currentLevel = 1;

    private void Awake()
    {
        EndgameObjects.ForEach(i => i.SetActive(false));
    }

    private void Update()
    {
        if (!_endGame) return;
        if (Input.GetKeyDown(KeyCode.End))
        {
            lch.LoadLevel();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();

        if (!_endGame && p != null)
        {
            ShowEndGame();
        }
    }

    private void ShowEndGame()
    {
        _endGame = true;
        EndgameObjects.ForEach(i => i.SetActive(true));

        foreach(var i in EndgameObjects)
        {
            i.SetActive(true);
            i.transform.DOScale(0, .2f).SetEase(Ease.OutBack).From();
        }
        SaveManager.Instance.LevelFinished();
    }
}
