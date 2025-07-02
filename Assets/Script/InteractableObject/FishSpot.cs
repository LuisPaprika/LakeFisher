using System;
using System.Collections;
using UnityEngine;

public class FishSpot : MonoBehaviour
{
    public void Fishing()
    {
        StartCoroutine(fishCountdown(10f));
    }

    private IEnumerator fishCountdown(float duration)
    {
        float startTime = 0f;

        while (startTime < duration)
        {
            if (!PlayerControl.isFishing)
            {
                break;
            }
            Debug.Log("Fish eating");
            startTime += Time.deltaTime;
            StartCoroutine(moveObject());
            yield return null;
        }

        if (PlayerControl.isFishing)
        {
            Debug.Log("Fish bite the bait");
            PlayerControl.isFishing = false;
        }
        else
        {
            Debug.Log("Fish escaped");
        }

    }

    private IEnumerator moveObject()
    {
        Vector3 moveDirection;
        bool goLeft = UnityEngine.Random.Range(0, 2) == 0;
        while (PlayerControl.isFishing)
        {
            Debug.Log("Start New Direction: " + (goLeft ? "Left" : "Right"));
            if (goLeft)
            {
                moveDirection = Vector3.back;
            }
            else
            {
                moveDirection = Vector3.forward;
            }
            goLeft = !goLeft;

            float startTime = 0f;
            float moveSpeed = 0.1f;
            int duration = 2;
            Debug.Log("Start");
            while (startTime < duration)
            {
                startTime += Time.deltaTime;
                gameObject.transform.position += moveDirection * moveSpeed * Time.deltaTime;
                yield return null;
            }
            Debug.Log("End:" + startTime);
        }
    }

}
