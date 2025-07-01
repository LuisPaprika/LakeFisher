using System;
using System.Collections;
using UnityEngine;

public class FishSpot : MonoBehaviour
{
    public void Fishing()
    {
        StartCoroutine(fishCountdown(3f));
    }

    private IEnumerator fishCountdown(float duration)
    {
        float startTime = 0f;

        while (startTime < duration && PlayerControl.isFishing)
        {
            startTime += Time.deltaTime;
            Debug.Log("Time passed: " + startTime);
            yield return null;
        }

        if (PlayerControl.isFishing)
        {
            Debug.Log("Fish eating bait");
        }
        else
        {
            Debug.Log("Fish escaped");
        }
        
    }
}
