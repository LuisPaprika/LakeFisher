using System;
using UnityEngine;

public class SceneInit : MonoBehaviour
{
    public static event Action onSceneLoad;
    void Awake()
    {
        onSceneLoad?.Invoke();
    }
}
