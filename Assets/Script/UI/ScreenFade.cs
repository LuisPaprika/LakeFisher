using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenFade : MonoBehaviour
{
    private Animator fadeAnimator;
    void Awake()
    {
        fadeAnimator = GetComponent<Animator>();
        fadeIn();
        SceneInit.onSceneLoad += fadeIn;
        BoatInteract.onBoatInteract += loadScene;
        FishSpotSpawner.onSceneLoad += fadeIn;
        DayController.onNewDayStart += startFade;
    }

    private void fadeIn()
    {
        StartCoroutine(fadeEnter());
    }

    private void loadScene(string sceneName)
    {
        StartCoroutine(fadeToScene(sceneName));
    }

    public void startFade()
    {
        StartCoroutine(fadingScreen());
    }

    private IEnumerator fadeEnter()
    {
        fadeAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(3f);
    }

    private IEnumerator fadingScreen()
    {
        fadeAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(3f);
        fadeAnimator.SetTrigger("FadeOut");
    }

    private IEnumerator fadeToScene(string sceneName)
    {
        fadeAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(sceneName);
    }

    void OnDestroy()
    {
        BoatInteract.onBoatInteract -= loadScene;
        FishSpotSpawner.onSceneLoad -= fadeIn;
        SceneInit.onSceneLoad -= fadeIn;
        DayController.onNewDayStart -= startFade;
    }
}
