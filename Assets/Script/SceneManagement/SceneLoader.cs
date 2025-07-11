using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void goToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
