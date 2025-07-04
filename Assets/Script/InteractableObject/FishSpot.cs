using System.Collections;
using UnityEngine;

public class FishSpot : MonoBehaviour
{
    [SerializeField] float fishingTime = 5f;
    [SerializeField] float moveSpeed = 2f; //default 1
    private bool finishedMoving;
    private bool goForward;
    private int moveDuration;

    void Awake()
    {
        finishedMoving = true;
        goForward = Random.Range(0, 2) == 0 ? false : true;
    }

    void Update()
    {
        if (finishedMoving)
        {
            moveDuration = Random.Range(1, 3);
            StartCoroutine(moveObject());
        }
    }
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
        finishedMoving = false;
        float startTime = 0f;

        while (startTime <= moveDuration)
        {
            startTime += Time.deltaTime;
            if (goForward)
            {
                gameObject.transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
            }
            else
            {
                gameObject.transform.position += Vector3.back * Time.deltaTime * moveSpeed;
            }
            yield return null;
        }
        finishedMoving = true;
        goForward = !goForward;
    }

}
