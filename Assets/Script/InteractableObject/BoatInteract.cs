using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoatInteract : InteractableBase
{
    [Header("Target Scene Name")]
    [SerializeField] string sceneName;
    public override void Interact()
    {
        SceneManager.LoadScene(sceneName);
    }

}
