using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointBase : MonoBehaviour
{

    public MeshRenderer meshRenderer;
    public ParticleSystem partSystem;

    public int key = 01;


    private bool checkpointActivated = false;
    private string checkpointKey = "CheckpointKey";


    private void OnTriggerEnter(Collider other)
    {
        if(!checkpointActivated && other.transform.tag == "Player")
        {
            CheckCheckpoint();
        }
    }

    private void CheckCheckpoint()
    {
        TurnItOn();
        SaveCheckpoint();
    }

    private void TurnItOn()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.white);
        partSystem.Play();
    }
    private void TurnItOff()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.grey);
    }

    private void SaveCheckpoint()
    {
        /*if(PlayerPrefs.GetInt(checkpointKey, 0) > key)
            PlayerPrefs.SetInt(checkpointKey, key);*/

        CheckpointManager.Instance.SaveCheckPoint(key);

        checkpointActivated = true;
    }
}
