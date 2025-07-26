using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    private int index = 0;
    private static InputSystem_Actions inputActions;
    [SerializeField] private DialogueSO introDialogue;
    [SerializeField] private TMP_Text textBox;
    void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Conversation.Enable();
        inputActions.Conversation.NextDialouge.performed += nextText;
        textBox.text = introDialogue.Dialogues[index];
    }

    private void nextText(InputAction.CallbackContext context)
    {
        index++;
        if (index == introDialogue.Dialogues.Count)
        {
            SceneManager.LoadScene("CabinScene");
            return;
        }
        textBox.text = introDialogue.Dialogues[index];
    }

}   
