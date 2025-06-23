using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Linq;
using System.Runtime.CompilerServices;

public class InteractPrompt : MonoBehaviour
{
    private InputSystem_Actions inputActions;
    private string InteractKey;
    void Awake()
    {
        inputActions = new InputSystem_Actions();
        InteractableBase.OnHover += ShowPrompt;

        InputAction InteractAction = inputActions.FindAction("Interact");
        InteractKey = InteractAction.bindings.FirstOrDefault().path;
        InteractKey = InteractKey.Substring(InteractKey.IndexOf("/") + 1);
    }

    void Update()
    {
        ResetPrompt();
    }

    private void ShowPrompt(string text)
    {
        TMP_Text textUI = gameObject.GetComponent<TMP_Text>();
        textUI.text = "Press " + InteractKey.ToUpper() + " to " + text;
    }

    void ResetPrompt()
    {
        TMP_Text textUI = gameObject.GetComponent<TMP_Text>();
        textUI.text = "";
    }
}
