using UnityEngine;

public class ArrowButtonAssets : MonoBehaviour
{
    public static ArrowButtonAssets Instance;
    void Awake()
    {
        Instance = this;
    }
    public Sprite upArrow;
    public Sprite downArrow;
    public Sprite leftArrow;
    public Sprite rightArrow;

}

