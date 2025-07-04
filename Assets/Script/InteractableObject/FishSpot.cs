using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FishSpot : MonoBehaviour
{
    [SerializeField] float fishingTime = 5f;
    [SerializeField] float moveSpeed = 1.5f; //default 1.5
    public void Fishing()
    {
        PlayerControl.castLineAtFish = true;
        PlayerControl.isFishing = true;
        StartCoroutine(fishCountdown(fishingTime));
    }

    private IEnumerator fishCountdown(float time)
    {
        float currentTime = 0f;
        while (PlayerControl.isFishing)
        {
            Debug.Log("Time passed:" + currentTime);
            moveObject('R');
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

    private void moveObject(char direction)
    {
        if (direction == 'R')
        {
            gameObject.transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
        }
        else if (direction == 'L')
        {
            gameObject.transform.position += Vector3.back * Time.deltaTime * moveSpeed;
        }
        else
        {
            Debug.LogError("moveObject parameter needs to be 'R' or 'L'");
        }
        
    }

}
