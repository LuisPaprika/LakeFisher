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
            gameObject.transform.position += Vector3.forward * Time.deltaTime; //Moving fishspot
            Debug.Log("Fish Eating: " + startTime);
            yield return null;
        }

        if (PlayerControl.isFishing)
        {
            Debug.Log("Fish bite the bait");
        }
        else
        {
            Debug.Log("Fish escaped");
        }

    }

}
