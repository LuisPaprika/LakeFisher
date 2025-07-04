using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Linq;
using System.Runtime.CompilerServices;

public class InteractPrompt : MonoBehaviour
{
    private string InteractKey;
    void Awake()
    {
        InteractableBase.OnHover += ShowPrompt;
    }

    void Update()
    {
        ResetPrompt();
    }

    private void ShowPrompt(string text)
    {
        InputAction InteractAction = PlayerControl.inputActions.FindAction("Interact");
        
        InteractKey = InteractAction.bindings.FirstOrDefault().path;
        InteractKey = InteractKey.Substring(InteractKey.IndexOf("/") + 1);

        TMP_Text textUI = gameObject.GetComponent<TMP_Text>();
        textUI.text = "Press " + InteractKey.ToUpper() + " to " + text;
    }

    void ResetPrompt()
    {
        TMP_Text textUI = gameObject.GetComponent<TMP_Text>();
        textUI.text = "";
    }
}
