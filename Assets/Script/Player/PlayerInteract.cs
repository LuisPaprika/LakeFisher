using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] float InteractRange = 2.5f;
    private Camera playerCamera;
    void Awake()
    {
        playerCamera = gameObject.GetComponentInChildren<Camera>();
    }

    void Update()
    {
        PlayerControl.isFishing = false;
        if (PlayerControl.inputActions.Player.enabled || PlayerControl.inputActions.Fishing.enabled)
        {
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, InteractRange))
            {
                if (hit.collider.TryGetComponent<InteractableBase>(out InteractableBase interactObj))
                {
                    interactObj.Hovered();

                    if (PlayerControl.inputActions.FindAction("Interact").WasPerformedThisFrame())
                    {
                        interactObj.Interact();
                    }
                }

                else if (hit.collider.TryGetComponent<FishSpot>(out FishSpot fishSpot))
                {
                    PlayerControl.isFishing = true;
                    if (PlayerControl.inputActions.FindAction("Fishing").WasPerformedThisFrame())
                    {
                        fishSpot.Fishing();
                    }

                }

            }
        }

    }

}

