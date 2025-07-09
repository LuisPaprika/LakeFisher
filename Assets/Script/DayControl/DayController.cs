using System.Collections.Generic;
using UnityEngine;

public class DayController : MonoBehaviour
{
    [SerializeField] GameObject fishSpotPrefab;
    private Vector3 spawnPostion = new Vector3(195.550003f,9.38000011f,216.880005f);
    public static DayController Instance;
    private Dictionary<int, int> goal; //Key is dayCount value is fishGoal
    private int dayCount = -1; //dayCount 0 is tutorial
    private int fishCount = 0;
    private int fishGoal;
    private GameObject fishSpotGameObj;

    void Awake()
    {
        BoatInteract.onBoatInteract += goFishing;
        FishSpotSpawner.onSceneLoad += createFishSpot;

        Instance = this;
        goal = new Dictionary<int, int>
        {
            { 0, 2 },
            { 1, 5 },
            { 2, 5 },
            { 3, 7 },
            { 4, 9 }
        };
    }


    public void addFish(int amount)
    {
        fishCount += amount;
        Debug.Log("Fish Count:" + fishCount);
        if (fishCount >= goal[dayCount])
        {
            fishCount = 0;
            Debug.Log("Let's end the day");
        }
    }

    private void goFishing()
    {
        dayCount++;
    }

    public void createFishSpot()
    {
        if (fishSpotGameObj != null)
        {
            Destroy(fishSpotGameObj);
            fishSpotGameObj = null;
        }

        fishSpotGameObj = Instantiate(fishSpotPrefab, spawnPostion, Quaternion.identity);
    }
}
