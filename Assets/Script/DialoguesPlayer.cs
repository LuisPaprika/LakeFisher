using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DialoguesPlayer : MonoBehaviour
{

    //Attach this to Empty Object
    [SerializeField] GameObject textBoxBG;
    private InputSystem_Actions inputActions;
    private TMP_Text textBox;
    private int dialogueIndex; //First line of dialogues
    private DialogueSO currentDialogue;
    void Awake()
    {
        inputActions = new InputSystem_Actions();
        PersonInteract.OnTalk += StartDialogue;
    }

    private void StartDialogue(DialogueSO dialogue)
    {
        currentDialogue = dialogue;
        dialogueIndex = 0;

        inputActions.Player.Disable();
        inputActions.Conversation.Enable();
        PlayerControl.EnableInput = false;
        PlayerInteract.EnableInteract = false;

        textBox = textBoxBG.GetComponentInChildren<TMP_Text>();
        textBoxBG.SetActive(true); //Show text box and BG

        textBox.text = currentDialogue.Dialogues[dialogueIndex]; //Set shown text

        inputActions.Conversation.NextDialouge.performed += NextDialogue;
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
        inputActions.Conversation.NextDialouge.performed -= NextDialogue;

        textBoxBG.SetActive(false); //Hide text box and BG

        inputActions.Conversation.Disable();
        inputActions.Player.Enable();
        PlayerControl.EnableInput = true;
        PlayerInteract.EnableInteract = true;
    }
    void OnDisable()
    {
        inputActions.Player.Enable();
        inputActions.Conversation.Disable();
    }
}
