using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FishFighting : MonoBehaviour
{
    [SerializeField] GameObject spritePrefab;
    [SerializeField] GameObject answerButtons;
    [SerializeField] GameObject inputButtons;
    [SerializeField] GameObject minigameUI;
    private List<Arrow.ArrowType> correctButtons = new List<Arrow.ArrowType>() { Arrow.ArrowType.Up, Arrow.ArrowType.Down, Arrow.ArrowType.Left, Arrow.ArrowType.Right };
    private List<Arrow.ArrowType> pressedButtons = new List<Arrow.ArrowType>();
    void Awake()
    {
        FishSpot.onFishBite += enableFishFight;
    }

    void Start()
    {
        foreach (Arrow.ArrowType action in correctButtons)
        {
            GameObject gameObj = Instantiate(spritePrefab, answerButtons.transform);
            if (gameObj.TryGetComponent<Image>(out Image image))
            {
                image.sprite = Arrow.GetSprite(action);
            }
        }
    }

    void Update()
    {
        if (PlayerControl.inputActions.FishFighting.Up.WasPerformedThisFrame())
        {
            pressedButtons.Add(Arrow.ArrowType.Up);
            GameObject gameObj = Instantiate(spritePrefab, inputButtons.transform);
            if (gameObj.TryGetComponent<Image>(out Image image))
            {
                image.sprite = Arrow.GetSprite(Arrow.ArrowType.Up);
            }
        }

        else if (PlayerControl.inputActions.FishFighting.Down.WasPerformedThisFrame())
        {
            pressedButtons.Add(Arrow.ArrowType.Down);
            GameObject gameObj = Instantiate(spritePrefab, inputButtons.transform);
            if (gameObj.TryGetComponent<Image>(out Image image))
            {
                image.sprite = Arrow.GetSprite(Arrow.ArrowType.Down);
            }
        }

        else if (PlayerControl.inputActions.FishFighting.Left.WasPerformedThisFrame())
        {
            pressedButtons.Add(Arrow.ArrowType.Left);
            GameObject gameObj = Instantiate(spritePrefab, inputButtons.transform);
            if (gameObj.TryGetComponent<Image>(out Image image))
            {
                image.sprite = Arrow.GetSprite(Arrow.ArrowType.Left);
            }
        }

        else if (PlayerControl.inputActions.FishFighting.Right.WasPerformedThisFrame())
        {
            pressedButtons.Add(Arrow.ArrowType.Right);
            GameObject gameObj = Instantiate(spritePrefab, inputButtons.transform);
            if (gameObj.TryGetComponent<Image>(out Image image))
            {
                image.sprite = Arrow.GetSprite(Arrow.ArrowType.Right);
            }
        }
    }

    private void enableFishFight()
    {
        PlayerControl.SetActionMapByName("FishFighting");
        minigameUI.SetActive(true);
    }

}