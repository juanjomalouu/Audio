using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(int sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void closeGame()
    {
        Application.Quit();
    }
}