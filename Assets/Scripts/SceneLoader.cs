using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// Loads the specified scene by its build index.
    /// </summary>
    /// <param name="sceneName">The build index of the scene to load.</param>
    public void LoadScene(int sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Closes the application.
    /// </summary>
    public void closeGame()
    {
        Application.Quit();
    }
}