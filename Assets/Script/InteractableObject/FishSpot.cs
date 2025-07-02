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

        while (startTime < duration && PlayerControl.isFishing)
        {
            startTime += Time.deltaTime;
            StartCoroutine(moveObject());
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

    private IEnumerator moveObject()
    {
        Vector3 moveDirection;
        int choice = UnityEngine.Random.Range(0, 2); //choice will be either 0 or 1
        bool goLeft = choice == 0 ? true : false;
        while (PlayerControl.isFishing)
        {
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
            float moveSpeed = UnityEngine.Random.Range(0.2f, 0.5f);
            float duration = UnityEngine.Random.Range(0.2f, 1f); //duration that gameObject will move in this direction
            while (startTime < duration && PlayerControl.isFishing)
            {
                startTime += Time.deltaTime;
                gameObject.transform.position += moveDirection * moveSpeed * Time.deltaTime;

                yield return null;
            }

        }
    }

}
