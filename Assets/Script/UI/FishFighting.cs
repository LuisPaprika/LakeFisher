using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishFighting : MonoBehaviour
{
    [SerializeField] GameObject spritePrefab;
    [SerializeField] GameObject answerButtons;
    [SerializeField] GameObject inputButtons;
    [SerializeField] GameObject minigameUI;
    private List<Arrow.ArrowType> correctButtonsList = new List<Arrow.ArrowType>() { Arrow.ArrowType.Up, Arrow.ArrowType.Down, Arrow.ArrowType.Left, Arrow.ArrowType.Right };
    private int index;
    void Awake()
    {
        FishSpot.onFishBite += enableFishFight;
    }

    void Update()
    {
        if (PlayerControl.inputActions.FishFighting.Up.WasPerformedThisFrame())
        {
            Arrow.ArrowType input = Arrow.ArrowType.Up;
            displayButtonAtGameObject(input, inputButtons);
            checkButton(input);
        }

        else if (PlayerControl.inputActions.FishFighting.Down.WasPerformedThisFrame())
        {
            Arrow.ArrowType input = Arrow.ArrowType.Down;
            displayButtonAtGameObject(input, inputButtons);
            checkButton(input);
        }

        else if (PlayerControl.inputActions.FishFighting.Left.WasPerformedThisFrame())
        {
            Arrow.ArrowType input = Arrow.ArrowType.Left;
            displayButtonAtGameObject(input, inputButtons);
            checkButton(input);
        }

        else if (PlayerControl.inputActions.FishFighting.Right.WasPerformedThisFrame())
        {
            Arrow.ArrowType input = Arrow.ArrowType.Right;
            displayButtonAtGameObject(input, inputButtons);
            checkButton(input);
        }
    }

    private void enableFishFight(int actionCounts)
    {
        index = -1;
        foreach (Arrow.ArrowType action in correctButtonsList)
        {
            displayButtonAtGameObject(action, answerButtons);
        }
        PlayerControl.SetActionMapByName("FishFighting");
        minigameUI.SetActive(true);
    }

    private void displayButtonAtGameObject(Arrow.ArrowType button, GameObject parent)
    {
        GameObject gameObj = Instantiate(spritePrefab, parent.transform);
        if (gameObj.TryGetComponent<Image>(out Image image))
        {
            image.sprite = Arrow.GetSprite(button);
        }
    }

    private void clearObjectChildren(Transform gameobjTransform)
    {
        foreach (Transform child in gameobjTransform)
        {
            Destroy(child.gameObject);
        }
    }

    private void checkButton(Arrow.ArrowType button)
    {
        index++;
        if (button != correctButtonsList[index])
        {
            minigameUI.SetActive(false);
            clearObjectChildren(inputButtons.transform);
            clearObjectChildren(answerButtons.transform);
            PlayerControl.SetActionMapByName("Fishing");
        }
    }
}