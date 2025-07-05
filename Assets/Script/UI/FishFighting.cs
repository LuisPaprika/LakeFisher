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
            Arrow.ArrowType input = Arrow.ArrowType.Up;
            displayInput(input);
        }

        else if (PlayerControl.inputActions.FishFighting.Down.WasPerformedThisFrame())
        {
            Arrow.ArrowType input = Arrow.ArrowType.Down;
            displayInput(input);
        }

        else if (PlayerControl.inputActions.FishFighting.Left.WasPerformedThisFrame())
        {
            Arrow.ArrowType input = Arrow.ArrowType.Left;
            displayInput(input);
        }

        else if (PlayerControl.inputActions.FishFighting.Right.WasPerformedThisFrame())
        {
            Arrow.ArrowType input = Arrow.ArrowType.Right;
            displayInput(input);
        }
    }

    private void enableFishFight()
    {
        PlayerControl.SetActionMapByName("FishFighting");
        minigameUI.SetActive(true);
    }

    private void displayInput(Arrow.ArrowType input)
    {
        pressedButtons.Add(input);
        GameObject gameObj = Instantiate(spritePrefab, inputButtons.transform);
        if (gameObj.TryGetComponent<Image>(out Image image))
        {
            image.sprite = Arrow.GetSprite(input);
        }
    }

}