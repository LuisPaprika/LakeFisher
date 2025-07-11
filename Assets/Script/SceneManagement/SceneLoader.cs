using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] GameObject UI;
    public void goToScene(string sceneName)
    {
        UI.SetActive(!UI.activeSelf);
        SceneManager.LoadScene(sceneName);
    }
}
