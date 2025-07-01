using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BoatInteract : InteractableBase
{
    [Header("Target Scene Name")]
    [SerializeField] string sceneName;
    [Header("Preferred Action Map after scene change")]
    [SerializeField] string newActionMapName = "Player";
    public override void Interact()
    {
        PlayerControl.SetActionMapByName(newActionMapName);
        SceneManager.LoadScene(sceneName);
    }

}
