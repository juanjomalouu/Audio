using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //Cargar escena
    public void LoadScene(int sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    //Cerrar aplicación
    public void closeGame()
    {
        Application.Quit();
    }
}