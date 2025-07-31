using UnityEngine;

public class Jumpscare : MonoBehaviour
{
    [SerializeField] private Animator ghostAnimator;
    [SerializeField] private AudioSource jumpscareSource;
    private bool jumpScarable = false;
    void Awake()
    {
        DayController.onCreateJumpScare += enableJumpscare;
    }

    private void enableJumpscare()
    {
        jumpScarable = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (jumpScarable)
        {
            jumpscareSource.Play();
            ghostAnimator.SetTrigger("Jumpscare");
        }
    }

    void OnDestroy()
    {
        DayController.onCreateJumpScare -= enableJumpscare;
    }

}
