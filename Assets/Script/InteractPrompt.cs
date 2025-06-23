using UnityEngine;
using TMPro;

public class InteractPrompt : MonoBehaviour
{
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
        TMP_Text textUI = gameObject.GetComponent<TMP_Text>();
        textUI.text = text;
    }

    void ResetPrompt()
    {
        TMP_Text textUI = gameObject.GetComponent<TMP_Text>();
        textUI.text = "";
    }
}
