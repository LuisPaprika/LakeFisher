using System.Collections.Generic;
using UnityEngine;

public class DayController : MonoBehaviour
{
    [SerializeField] GameObject fishSpotPrefab;
    private List<Vector3> spawnPositionsList = new List<Vector3>()
    {
        new Vector3(204.919998f,9.39999962f,216.600006f),
        new Vector3(201.369995f,9.39999962f,210.630005f),
        new Vector3(195.029999f,9.39999962f,216.660004f),
        new Vector3(200.869995f,9.39999962f,223.059998f)
    };
    public static DayController Instance;
    private Dictionary<int, int> goal; //Key is dayCount value is fishGoal
    private int dayCount = 1; //dayCount 0 is tutorial
    private int fishCount = 0;
    private GameObject fishSpotGameObj;
    private bool doneFishing = false;
    private Vector3 spawnPostion;

    void Awake()
    {
        FishSpotSpawner.onSceneLoad += createFishSpot;
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

    }


    public void addFish(int amount)
    {
        fishCount += amount;
        Debug.Log("Fish Count:" + fishCount);
        if (fishCount >= goal[dayCount])
        {
            doneFishing = true;
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
            Debug.Log("I still need to fish. Today is "+ dayCount);
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
            spawnPostion = spawnPositionsList[Random.Range(0, 4)];
            fishSpotGameObj = Instantiate(fishSpotPrefab, spawnPostion, Quaternion.identity);
        }

    }

    private void jumpScare()
    {
        switch (dayCount)
        {
            case 1:
                if (fishCount == 3)
                {
                    Debug.Log("Play Jumpscare 1");
                }
                break;
            case 2:
                if (fishCount == 5)
                {
                    Debug.Log("Play Jumpscare 2");
                }
                break;
            case 3:
                if (fishCount == 7)
                {
                    Debug.Log("Play Jumpscare 3");
                }
                break;
            case 4:
                if (fishCount == 9)
                {
                    Debug.Log("Play Jumpscare 4");
                }
                break;

        }
    }
}
