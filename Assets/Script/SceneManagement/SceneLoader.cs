using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public void goToScene(string sceneName)
    {
        StartCoroutine(loadScene(sceneName));
    }

    private IEnumerator loadScene(string sceneName)
    {
        animator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(sceneName);
    }
}
