using UnityEngine;

public class DrawerInteract : InteractableBase
{
    [SerializeField] Animator DrawerAnimator;
    private bool interacted = false;
    public override void Interact()
    {
        interacted = !interacted;
        if (interacted)
        {
            DrawerAnimator.SetTrigger("OpenDrawer");
        }
        else
        {
            DrawerAnimator.SetTrigger("CloseDrawer");
        }
    }
}
