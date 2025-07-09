using UnityEngine;

public class Arrow : MonoBehaviour
{
    public enum ArrowType
    {
        Up,
        Down,
        Left,
        Right
    }

    public static Sprite GetSprite(ArrowType input)
    {
        switch (input)
        {
            case ArrowType.Up:
                return ArrowButtonAssets.Instance.upArrow;
            case ArrowType.Down:
                return ArrowButtonAssets.Instance.downArrow;
            case ArrowType.Left:
                return ArrowButtonAssets.Instance.leftArrow;
            case ArrowType.Right:
                return ArrowButtonAssets.Instance.rightArrow;
        }
        return null;
    }
}


