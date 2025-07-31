using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishFighting : MonoBehaviour
{
    [SerializeField] GameObject dayController;
    [SerializeField] GameObject spritePrefab;
    [SerializeField] GameObject answerButtons;
    [SerializeField] GameObject inputButtons;
    [SerializeField] GameObject minigameUI;
    [SerializeField] private DialogueSO fishEscapedDialogue;
    public static event Action<DialogueSO, string> onExitFishFight;
    private List<Arrow.ArrowType> correctButtonsList = new List<Arrow.ArrowType>();
    private int index;
    private DayController dayControllerScript;
    void Awake()
    {
        DayController.onTimerEnd += exitFishFight;
        DayController.onStartFightTimer += enableFishFight;
        dayControllerScript = dayController.GetComponent<DayController>();
    }

    void Update()
    {
        if (PlayerControl.inputActions != null)
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


    }

    private void enableFishFight(int actionCounts) //start fish fighting
    {
        correctButtonsList.Clear();
        for (int i = 0; i < actionCounts; i++)
        {
            int direction = UnityEngine.Random.Range(1, 4);
            switch (direction)
            {
                case 1:
                    correctButtonsList.Add(Arrow.ArrowType.Up);
                    break;
                case 2:
                    correctButtonsList.Add(Arrow.ArrowType.Down);
                    break;
                case 3:
                    correctButtonsList.Add(Arrow.ArrowType.Left);
                    break;
                case 4:
                    correctButtonsList.Add(Arrow.ArrowType.Right);
                    break;
            }
        }

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
        if (button != correctButtonsList[index]) //pressed wrong button
        {
            exitFishFight();
            dayControllerScript.createFishSpot();
            onExitFishFight.Invoke(fishEscapedDialogue, "Fishing");
        }
        else if (index == correctButtonsList.Count - 1) //finish fish fighting
        {
            dayControllerScript.addFish(1);
            exitFishFight();
            dayControllerScript.createFishSpot();
        }
    }

    private void exitFishFight()
    {
        minigameUI.SetActive(false);
        clearObjectChildren(inputButtons.transform);
        clearObjectChildren(answerButtons.transform);
    }

    void OnDestroy()
    {
        DayController.onTimerEnd -= exitFishFight;
        DayController.onStartFightTimer -= enableFishFight;
    }
}