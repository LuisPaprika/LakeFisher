using System;
using UnityEngine;

public class FishSpotSpawner : MonoBehaviour
{
    public static event Action onSceneLoad;
    void Awake()
    {
        onSceneLoad.Invoke();
    }
}
