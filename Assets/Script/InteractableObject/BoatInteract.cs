using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BoatInteract : InteractableBase
{
    private enum ActionMap
    {
        Player,
        UI,
        Conversation,
        Fishing
    }
    private InputSystem_Actions inputActions;
    [Header("Target Scene Name")]
    [SerializeField] string sceneName;
    [Header("Preferred Action Map after scene change")]
    [SerializeField] string newActionMapName = "Player";
    void Awake()
    {
        inputActions = new InputSystem_Actions();
    }
    public override void Interact()
    {
        inputActions.Disable();
        SceneManager.LoadScene(sceneName);
    }
}
