using System;
using System.Collections;
using UnityEngine;

public class FishSpot : MonoBehaviour
{
    [SerializeField] GameObject fishingGauge;
    public float fishingTime;
    public float moveSpeed;
    public static event Action onFishBite;
    private bool finishedMoving;
    private bool goForward;
    private int moveDuration;
    private float currentTime;

    void Awake()
    {
        finishedMoving = true;
        goForward = UnityEngine.Random.Range(0, 2) == 0 ? false : true;
    }

    void Update()
    {
        if (finishedMoving)
        {
            moveDuration = UnityEngine.Random.Range(1, 3);
            StartCoroutine(moveObject());
        }
    }

    public void Fishing()
    {
        StartCoroutine(fishCountdown(fishingTime));
    }



    private IEnumerator fishCountdown(float time)
    {
        float t;
        while (PlayerControl.castLineAtFish)
        {
            currentTime += Time.deltaTime;
            t = Mathf.Clamp01(currentTime / time);
            if (PlayerControl.isFishing)
            {
                fishingGauge.transform.localScale = Vector3.Lerp(fishingGauge.transform.localScale, new Vector3(1, 1, 1), t);
                if (fishingGauge.transform.localScale == new Vector3(1, 1, 1))
                {
                    onFishBite.Invoke();
                    PlayerControl.castLineAtFish = false;
                    currentTime = 0f;
                }
            }
            else
            {
                fishingGauge.transform.localScale = Vector3.Lerp(fishingGauge.transform.localScale, new Vector3(1, 0, 1), 0.025f);
                if (fishingGauge.transform.localScale == new Vector3(1, 0, 1))
                {
                    PlayerControl.castLineAtFish = false;
                    currentTime = 0f;
                    Debug.Log("Fish Escaped");
                }
            }
            yield return null;
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
