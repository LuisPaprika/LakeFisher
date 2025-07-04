using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DialoguesPlayer : MonoBehaviour
{
    //Attach this to UI (Canvas)
    [SerializeField] GameObject textBoxBG;
    private TMP_Text textBox;
    private int dialogueIndex; //First line of dialogues
    private DialogueSO currentDialogue;
    void Awake()
    {
        PersonInteract.OnTalk += StartDialogue;
    }

    private void StartDialogue(DialogueSO dialogue, Vector3 vector3)
    {
        PlayerControl.SetActionMapByName("Conversation");
        currentDialogue = dialogue;
        dialogueIndex = 0;

        textBox = textBoxBG.GetComponentInChildren<TMP_Text>();
        textBoxBG.SetActive(true); //Show text box and BG

        textBox.text = currentDialogue.Dialogues[dialogueIndex]; //Set shown text

        PlayerControl.inputActions.Conversation.NextDialouge.performed += NextDialogue;
    }

    private void NextDialogue(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        dialogueIndex++;
        if (dialogueIndex < currentDialogue.Dialogues.Count)
        {
            textBox.text = currentDialogue.Dialogues[dialogueIndex];
        }
        else
        {
            EndDialogue();
        }

    }

    private void EndDialogue()
    {
        PlayerControl.inputActions.Conversation.NextDialouge.performed -= NextDialogue;

        textBoxBG.SetActive(false); //Hide text box and BG
        PlayerControl.SetActionMapByName("Player");
    }
}
