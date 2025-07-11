using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] GameObject UI;
    public void goToScene(string sceneName)
    {
        UI.SetActive(true);
        SceneManager.LoadScene(sceneName);
    }
}
