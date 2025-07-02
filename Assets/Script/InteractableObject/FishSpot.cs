using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FishSpot : MonoBehaviour
{
    [SerializeField] float targetTime = 5f;
    public void Fishing()
    {
        PlayerControl.castLineAtFish = true;
        PlayerControl.isFishing = true;
        StartCoroutine(fishCountdown(targetTime));
    }

    private IEnumerator fishCountdown(float time)
    {
        float currentTime = 0f;
        while (PlayerControl.isFishing)
        {
            Debug.Log("Time passed:" + currentTime);
            currentTime += Time.deltaTime;
            if (currentTime > time)
            {
                break;
            }

            yield return null;
        }

        if (PlayerControl.isFishing) //Player track the fish for target time
        {
            Debug.Log("Fish bite");
        }
        else //Player failed to track fish
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
