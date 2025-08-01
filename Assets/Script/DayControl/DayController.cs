using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class DayController : MonoBehaviour
{
    [SerializeField] GameObject fishSpotPrefab;
    [SerializeField] GameObject timer;
    [SerializeField] TMP_Text dayCounter;
    [SerializeField] TMP_Text fishCounter;
    [SerializeField] private DialogueSO needFishDialogue;
    [SerializeField] private DialogueSO fishCaughtDialogue;
    [SerializeField] private DialogueSO enoughFishDialogue;
    [SerializeField] private AudioSource clotheSFX;
    [SerializeField] private AudioSource fishCaughtSFX;
    [SerializeField] private AudioSource jumpScareSource;
    [SerializeField] private AudioClip[] jumpSoundClip;
    public static event Action onCreateJumpScare;
    public static event Action onNewDayStart;
    public static event Action<DialogueSO, string> onFishCaught;
    public static event Action<DialogueSO, string> onEnoughFish;
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
    public static event Action onSleep;
    public static event Action<int> onStartFightTimer;
    public static event Action<DialogueSO, string> onNeedToFish;
    private Dictionary<int, int> goal; //Key is dayCount value is fishGoal
    private int dayCount = 0; //dayCount 0 is tutorial
    private int fishCount = 0;
    private GameObject fishSpotGameObj;
    private bool doneFishing = false;
    private Vector3 maxScale;
    private int i = 0;


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
            { 1, 5 },
            { 2, 6 },
            { 3, 7 },
            { 4, 13 }
        };

        maxScale = timer.transform.localScale;
    }

    public void addFish(int amount)
    {
        fishCaughtSFX.Play();
        fishCount += amount;
        fishCounter.text = "Fish : " + fishCount.ToString() + " / " + goal[dayCount];
        onFishCaught.Invoke(fishCaughtDialogue, "Fishing");
        resetTimer(fishCaughtDialogue, "");
        if (fishCount >= goal[dayCount])
        {
            doneFishing = true;
            onEnoughFish.Invoke(enoughFishDialogue, "Fishing");
        }

        if (jumpScarable()) //Wait around 2 sec before jump
        {
            Debug.Log("Jump Scared");
            StartCoroutine(jumpSound());
        }
    }

    private void goToNextDay()
    {
        if (doneFishing)
        {
            dayCount++;
            dayCounter.text = "Day : " + dayCount.ToString();
            fishCount = 0;
            fishCounter.text = "Fish : " + fishCount.ToString() + " / " + goal[dayCount];
            doneFishing = false;

            PlayerControl.inputActions.Player.Disable();
            onSleep.Invoke();
            onNewDayStart.Invoke();
            if (dayCount == 3)
            {
                onCreateJumpScare.Invoke();
            }
            
        }
        else
        {
            onNeedToFish.Invoke(needFishDialogue, "Player");
        }
    }

    public void createFishSpot()
    {
        if (fishSpotGameObj != null)
        {
            Destroy(fishSpotGameObj);
            fishSpotGameObj = null;
        }



        if (!doneFishing) //Creating fishSpot and setting its values
        {
            create();
            //StartCoroutine(waitToSpawnFish(UnityEngine.Random.Range(5, 8)));
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
                if (fishCount == 4)
                {
                    return true;
                }
                break;
            case 3:
                if (fishCount == 6)
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
                onStartFightTimer.Invoke(actionCounts);
                StartCoroutine(IncreasingTimer(timer));
                break;
            case 1:
                time = 10;
                actionCounts = 5;
                onStartFightTimer.Invoke(actionCounts);
                StartCoroutine(IncreasingTimer(timer));
                break;
            case 2:
                time = 10;
                actionCounts = 6;
                onStartFightTimer.Invoke(actionCounts);
                StartCoroutine(IncreasingTimer(timer));
                break;
            case 3:
                time = 10;
                actionCounts = 8;
                onStartFightTimer.Invoke(actionCounts);
                StartCoroutine(IncreasingTimer(timer));
                break;
            case 4:
                time = 4;
                actionCounts = 12;
                onStartFightTimer.Invoke(actionCounts);
                StartCoroutine(IncreasingTimer(timer));
                break;
        }
    }

    private void resetTimer(DialogueSO temp, string temp2)
    {
        time = 0;
    }
    private float getFishingTimeFromDay()
    {
        switch (dayCount)
        {
            case 0:
                return 50f;
            case 1:
                return 45f;
            case 2:
                return 40f;
            case 3:
                return 35f;
            case 4:
                return 18f;
            default:
                return 10f;
        }
    }

    void OnDestroy()
    {
        FishSpotSpawner.onSceneLoad -= createFishSpot;
        FishFighting.onExitFishFight -= resetTimer;
        FishSpot.onFishBite -= setTime;
        BedInteract.onSleep -= goToNextDay;
    }

    private IEnumerator IncreasingTimer(GameObject timer)
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

    private IEnumerator waitToSpawnFish(int sec)
    {
        yield return new WaitForSeconds(sec);
        create();
    }

    private void create()
    {
        Vector3 spawnPostion = spawnPositionsList[UnityEngine.Random.Range(0, 4)];
        fishSpotGameObj = Instantiate(fishSpotPrefab, spawnPostion, Quaternion.identity);
        FishSpot fishSpotScript = fishSpotGameObj.GetComponent<FishSpot>();
        fishSpotScript.fishingTime = getFishingTimeFromDay();
        fishSpotScript.moveSpeed = 1f;
    }

    private IEnumerator jumpSound()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 3f));
        jumpScareSource.clip = jumpSoundClip[i];
        jumpScareSource.Play();
        i++;

        if (i >= jumpSoundClip.Length)
        {
            i = 0;
        }
    }
}
