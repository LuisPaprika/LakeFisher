using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayController : MonoBehaviour
{
    [SerializeField] GameObject fishSpotPrefab;
    [SerializeField] GameObject timer;
    private float time;
    private List<Vector3> spawnPositionsList = new List<Vector3>()
    {
        new Vector3(204.919998f,9.39999962f,216.600006f),
        new Vector3(201.369995f,9.39999962f,210.630005f),
        new Vector3(195.029999f,9.39999962f,216.660004f),
        new Vector3(200.869995f,9.39999962f,223.059998f)
    };
    public static DayController Instance;
    public static event Action onTimerEnd;
    public static event Action<int> onStartTimer;
    private Dictionary<int, int> goal; //Key is dayCount value is fishGoal
    private int dayCount = 1; //dayCount 0 is tutorial
    private int fishCount = 0;
    private GameObject fishSpotGameObj;
    private bool doneFishing = false;
    private Vector3 maxScale;


    void Awake()
    {
        FishSpotSpawner.onSceneLoad += createFishSpot;
        FishFighting.onExitFishFight += resetTimer;
        FishSpot.onFishBite += setTime;
        BedInteract.onSleep += goToNextDay;

        Instance = this;
        goal = new Dictionary<int, int>
        {
            { 0, 3 },
            { 1, 7 },
            { 2, 7 },
            { 3, 10 },
            { 4, 13 }
        };

        maxScale = timer.transform.localScale;

    }


    public void addFish(int amount)
    {
        fishCount += amount;
        Debug.Log("Fish Count:" + fishCount);
        if (fishCount >= goal[dayCount])
        {
            doneFishing = true;
        }

        if (jumpScarable())
        {
            Debug.Log("Jump Scared");
        }
    }

    private void goToNextDay()
    {
        if (doneFishing)
        {
            dayCount++;
            fishCount = 0;
            Debug.Log("This is day " + dayCount);
        }
        else
        {
            Debug.Log("I still need to fish. Today is " + dayCount);
        }
    }

    public void createFishSpot()
    {

        if (fishSpotGameObj != null)
        {
            Destroy(fishSpotGameObj);
            fishSpotGameObj = null;
        }

        if (!doneFishing)
        {
            Vector3 spawnPostion = spawnPositionsList[UnityEngine.Random.Range(0, 4)];
            fishSpotGameObj = Instantiate(fishSpotPrefab, spawnPostion, Quaternion.identity);
        }

    }

    private bool jumpScarable()
    {
        switch (dayCount)
        {
            case 1:
                if (fishCount == 3)
                {
                    return true;
                }
                break;
            case 2:
                if (fishCount == 5)
                {
                    return true;
                }
                break;
            case 3:
                if (fishCount == 7)
                {
                    return true;
                }
                break;
            case 4:
                if (fishCount == 9)
                {
                    return true;
                }
                break;
            default:
                break;
        }
        return false;
    }

    private void setTime()
    {
        int actionCounts;
        switch (dayCount)
        {
            case 0:
                time = 10;
                actionCounts = 4;
                onStartTimer.Invoke(actionCounts);
                StartCoroutine(startTimer());
                break;
            case 1:
                time = 8;
                actionCounts = 6;
                onStartTimer.Invoke(actionCounts);
                StartCoroutine(startTimer());
                break;
            case 2:
                time = 7;
                actionCounts = 8;
                onStartTimer.Invoke(actionCounts);
                StartCoroutine(startTimer());
                break;
            case 3:
                time = 6;
                actionCounts = 10;
                onStartTimer.Invoke(actionCounts);
                StartCoroutine(startTimer());
                break;
            case 4:
                time = 4;
                actionCounts = 12;
                onStartTimer.Invoke(actionCounts);
                StartCoroutine(startTimer());
                break;
        }
    }

    private void resetTimer()
    {
        time = 0;
    }

    private IEnumerator startTimer()
    {
        float startTime = 0f;
        while (startTime < time)
        {
            startTime += Time.deltaTime;
            float t = Mathf.Clamp01(startTime / time);
            timer.transform.localScale = Vector3.Lerp(maxScale, new Vector3(0, maxScale.y, maxScale.z), t);
            yield return null;
        }
        onTimerEnd.Invoke();
    }
}
