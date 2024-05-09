using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using TMPro;

public class CheckpointManager : Singleton<CheckpointManager>
{

    public int lastCheckPointKey = 0;
    public TextMeshProUGUI tmp;

    public List<CheckPointBase> checkpoints;

    public bool HasCheckpoint()
    {
        return lastCheckPointKey > 0;
    }

    private void ShowCheckpointUI(string s)
    {
        tmp.SetText("Checkpoint " + s + " reached!");
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float duration = 2f; //Fade out over 2 seconds.
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(2f, 0f, currentTime / duration);
            tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        yield break;
    }

    public void SaveCheckPoint(int i)
    {
        if(i > lastCheckPointKey)
        {
            lastCheckPointKey = i;
            ShowCheckpointUI(lastCheckPointKey.ToString());
        }
    }

    public Vector3 GetPositionFromLastCheckpoint()
    {
        var checkpoint = checkpoints.Find(i => i.key == lastCheckPointKey);
        return checkpoint.transform.position;
    }
}
